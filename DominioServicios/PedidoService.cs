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
        private readonly PrecioCompraService _precioCompraService;


        public PedidoService(BuyJugadorContext context, PrecioCompraService precioCompraService)
        {
            _context = context;
            _precioCompraService = precioCompraService;
        }


        public async Task<List<PedidoDTO>> GetAllPedidosDetalladosAsync()
        {
            var pedidos = await _context.Pedidos
                .AsNoTracking()
                .Include(p => p.IdProveedorNavigation)
                .Include(p => p.LineasPedido)
                    .ThenInclude(lp => lp.IdProductoNavigation)
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




        public async Task<PedidoDTO> CrearPedidoCompletoAsync(CrearPedidoCompletoDTO crearPedidoDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nuevoPedido = new Pedido
                {
                    Fecha = DateTime.UtcNow,
                    Estado = "Pendiente",
                    IdProveedor = crearPedidoDto.IdProveedor
                };


                _context.Pedidos.Add(nuevoPedido);
                await _context.SaveChangesAsync(); 


                foreach (var lineaDto in crearPedidoDto.LineasPedido)
                {
                    var montoPrecioCompra = await _precioCompraService.GetMontoAsync(lineaDto.IdProducto, crearPedidoDto.IdProveedor);


                    if (!montoPrecioCompra.HasValue)
                    {
                        throw new InvalidOperationException($"No se encontró un precio de compra para el producto ID {lineaDto.IdProducto}");
                    }


                    var nuevaLinea = new LineaPedido
                    {
                        IdPedido = nuevoPedido.IdPedido,
                        IdProducto = lineaDto.IdProducto,
                        Cantidad = lineaDto.Cantidad,
                        PrecioUnitario = montoPrecioCompra.Value
                    };
                    _context.LineaPedidos.Add(nuevaLinea);


                    var producto = await _context.Productos.FindAsync(lineaDto.IdProducto);
                    if (producto != null)
                    {
                        producto.Stock += lineaDto.Cantidad;
                    }
                }


                await _context.SaveChangesAsync();
                await transaction.CommitAsync();


                return new PedidoDTO { IdPedido = nuevoPedido.IdPedido };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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

        public async Task UpdatePedidoCompletoAsync(int id, PedidoDTO pedidoDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pedido = await _context.Pedidos.Include(p => p.LineasPedido).FirstOrDefaultAsync(p => p.IdPedido == id);
                if (pedido == null) throw new KeyNotFoundException("Pedido no encontrado.");


                _context.LineaPedidos.RemoveRange(pedido.LineasPedido);


                foreach (var lineaDto in pedidoDto.LineasPedido)
                {
                    pedido.LineasPedido.Add(new LineaPedido
                    {
                        IdProducto = lineaDto.IdProducto,
                        Cantidad = lineaDto.Cantidad,
                        PrecioUnitario = lineaDto.PrecioUnitario
                    });
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
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) throw new KeyNotFoundException("Pedido no encontrado.");
            if (pedido.Estado == "Recibido") throw new InvalidOperationException("El pedido ya fue recibido.");


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


                if (pedido.Estado == "Pendiente")
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
    }
}
