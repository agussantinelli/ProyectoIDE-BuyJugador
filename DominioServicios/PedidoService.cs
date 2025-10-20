using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class PedidoService
    {
        private readonly BuyJugadorContext _context;

        public PedidoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<PedidoDTO>> GetAllPedidosDetalladosAsync()
        {
            var pedidos = await _context.Pedidos
               .AsNoTracking()
               .Include(p => p.IdProveedorNavigation)
               .Include(p => p.LineasPedido)
               .OrderByDescending(p => p.Fecha)
               .ToListAsync();

            return pedidos.Select(p => new PedidoDTO
            {
                IdPedido = p.IdPedido,
                Fecha = p.Fecha,
                Estado = p.Estado,
                IdProveedor = p.IdProveedor.GetValueOrDefault(),
                ProveedorRazonSocial = p.IdProveedorNavigation?.RazonSocial ?? "N/A",
                Total = p.LineasPedido.Sum(lp => lp.Cantidad * lp.PrecioUnitario)
            }).ToList();
        }

        public async Task<PedidoDTO?> GetPedidoDetalladoByIdAsync(int id)
        {
            var pedido = await _context.Pedidos
               .AsNoTracking()
               .Include(p => p.IdProveedorNavigation)
               .Include(p => p.LineasPedido)
                   .ThenInclude(lp => lp.IdProductoNavigation)
               .FirstOrDefaultAsync(p => p.IdPedido == id);

            if (pedido == null) return null;

            return new PedidoDTO
            {
                IdPedido = pedido.IdPedido,
                Fecha = pedido.Fecha,
                Estado = pedido.Estado,
                IdProveedor = pedido.IdProveedor.GetValueOrDefault(),
                ProveedorRazonSocial = pedido.IdProveedorNavigation?.RazonSocial ?? "N/A",
                Total = pedido.LineasPedido.Sum(lp => lp.Cantidad * lp.PrecioUnitario),
                LineasPedido = pedido.LineasPedido.Select(lp => new LineaPedidoDTO
                {
                    NroLineaPedido = lp.NroLineaPedido,
                    IdProducto = lp.IdProducto,
                    NombreProducto = lp.IdProductoNavigation?.Nombre ?? "N/A",
                    Cantidad = lp.Cantidad,
                    PrecioUnitario = lp.PrecioUnitario
                }).ToList()
            };
        }

        public async Task<PedidoDTO?> CrearPedidoCompletoAsync(CrearPedidoCompletoDTO crearPedidoDto)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var nuevoPedido = new Pedido
                    {
                        Fecha = DateTime.UtcNow,
                        Estado = crearPedidoDto.MarcarComoRecibido ? "Recibido" : "Pendiente",
                        IdProveedor = crearPedidoDto.IdProveedor
                    };
                    _context.Pedidos.Add(nuevoPedido);
                    await _context.SaveChangesAsync();

                    var idsProductos = crearPedidoDto.LineasPedido.Select(l => l.IdProducto).Distinct().ToList();
                    var productosAfectados = await _context.Productos
                        .Where(p => idsProductos.Contains(p.IdProducto) && p.Activo)
                        .ToDictionaryAsync(p => p.IdProducto);

                    var preciosCompra = await _context.PreciosCompra
                        .Where(pc => pc.IdProveedor == crearPedidoDto.IdProveedor && idsProductos.Contains(pc.IdProducto))
                        .ToDictionaryAsync(pc => pc.IdProducto, pc => pc.Monto);

                    int nroLinea = 1;
                    foreach (var lineaDto in crearPedidoDto.LineasPedido)
                    {
                        if (!productosAfectados.TryGetValue(lineaDto.IdProducto, out var producto) || !preciosCompra.TryGetValue(lineaDto.IdProducto, out var montoPrecioCompra))
                        {
                            throw new InvalidOperationException($"Datos inválidos para el producto ID {lineaDto.IdProducto}.");
                        }

                        _context.LineaPedidos.Add(new LineaPedido
                        {
                            IdPedido = nuevoPedido.IdPedido,
                            NroLineaPedido = nroLinea++,
                            IdProducto = lineaDto.IdProducto,
                            Cantidad = lineaDto.Cantidad,
                            PrecioUnitario = montoPrecioCompra
                        });

                        if (crearPedidoDto.MarcarComoRecibido)
                        {
                            producto.Stock += lineaDto.Cantidad;
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return await GetPedidoDetalladoByIdAsync(nuevoPedido.IdPedido);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error al crear pedido completo: {ex.Message}");
                    return null;
                }
            });
        }

        public async Task UpdatePedidoCompletoAsync(int id, PedidoDTO pedidoDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pedido = await _context.Pedidos
                    .Include(p => p.LineasPedido)
                    .FirstOrDefaultAsync(p => p.IdPedido == id);

                if (pedido == null) throw new KeyNotFoundException("Pedido no encontrado.");
                if (pedido.Estado != "Pendiente") throw new InvalidOperationException("Solo se pueden modificar pedidos en estado 'Pendiente'.");

                var idsProductosOriginales = pedido.LineasPedido.Select(l => l.IdProducto).ToList();
                var productosOriginales = await _context.Productos
                    .Where(p => idsProductosOriginales.Contains(p.IdProducto))
                    .ToDictionaryAsync(p => p.IdProducto);

                foreach (var lineaOriginal in pedido.LineasPedido)
                {
                    if (productosOriginales.TryGetValue(lineaOriginal.IdProducto, out var producto))
                    {
                        // Si el pedido no había sido recibido, no se había sumado stock, así que no se resta nada.
                    }
                }
                _context.LineaPedidos.RemoveRange(pedido.LineasPedido);
                await _context.SaveChangesAsync();

                var idsProductosNuevos = pedidoDto.LineasPedido.Select(l => l.IdProducto).ToList();
                var productosNuevos = await _context.Productos
                    .Where(p => idsProductosNuevos.Contains(p.IdProducto))
                    .ToDictionaryAsync(p => p.IdProducto);

                int nroLinea = 1;
                foreach (var lineaDto in pedidoDto.LineasPedido)
                {
                    if (productosNuevos.TryGetValue(lineaDto.IdProducto, out var producto))
                    {
                        _context.LineaPedidos.Add(new LineaPedido
                        {
                            IdPedido = pedido.IdPedido,
                            NroLineaPedido = nroLinea++,
                            IdProducto = lineaDto.IdProducto,
                            Cantidad = lineaDto.Cantidad,
                            PrecioUnitario = lineaDto.PrecioUnitario
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task MarcarComoRecibidoAsync(int id)
        {
            var pedido = await _context.Pedidos.Include(p => p.LineasPedido).FirstOrDefaultAsync(p => p.IdPedido == id);
            if (pedido == null) throw new KeyNotFoundException("Pedido no encontrado.");
            if (pedido.Estado == "Recibido") throw new InvalidOperationException("El pedido ya fue recibido.");

            var idsProductos = pedido.LineasPedido.Select(l => l.IdProducto).ToList();
            var productosAfectados = await _context.Productos.Where(p => idsProductos.Contains(p.IdProducto)).ToDictionaryAsync(p => p.IdProducto);

            foreach (var linea in pedido.LineasPedido)
            {
                if (productosAfectados.TryGetValue(linea.IdProducto, out var producto))
                {
                    producto.Stock += linea.Cantidad;
                }
            }

            pedido.Estado = "Recibido";
            await _context.SaveChangesAsync();
        }


        public async Task DeletePedidoCompletoAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pedido = await _context.Pedidos.Include(p => p.LineasPedido).FirstOrDefaultAsync(p => p.IdPedido == id);
                if (pedido == null) throw new KeyNotFoundException("Pedido no encontrado.");

                if (pedido.Estado == "Recibido")
                {
                    foreach (var linea in pedido.LineasPedido)
                    {
                        var producto = await _context.Productos.FindAsync(linea.IdProducto);
                        if (producto != null)
                        {
                            producto.Stock -= linea.Cantidad;
                            if (producto.Stock < 0) producto.Stock = 0;
                        }
                    }
                }

                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> GetCantidadPedidosPendientesAsync()
        {
            var cantidad = await _context.Pedidos
                .CountAsync(p => p.Estado == "Pendiente");
            
            return cantidad;
        }

    }
}
