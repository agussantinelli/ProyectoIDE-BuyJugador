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
                    .ThenInclude(lp => lp.IdProductoNavigation)
                        .ThenInclude(prod => prod.Precios)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();

            return pedidos.Select(p => new PedidoDTO
            {
                IdPedido = p.IdPedido,
                Fecha = p.Fecha,
                Estado = p.Estado,
                IdProveedor = p.IdProveedor.GetValueOrDefault(),
                ProveedorRazonSocial = p.IdProveedorNavigation?.RazonSocial ?? "N/A",
                Total = p.LineasPedido.Sum(lp => {
                    var precio = lp.IdProductoNavigation?.Precios?
                                   .OrderByDescending(pr => pr.FechaDesde)
                                   .FirstOrDefault(pr => pr.FechaDesde <= p.Fecha)?.Monto ?? 0;
                    return lp.Cantidad * precio;
                })
            }).ToList();
        }

        public async Task<PedidoDTO> CrearPedidoCompletoAsync(CrearPedidoCompletoDTO crearPedidoDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nuevoPedido = new Pedido
                {
                    Fecha = DateTime.Now,
                    Estado = "Pendiente",
                    IdProveedor = crearPedidoDto.IdProveedor
                };
                _context.Pedidos.Add(nuevoPedido);
                await _context.SaveChangesAsync();

                int nroLinea = 1;
                foreach (var lineaDto in crearPedidoDto.LineasPedido)
                {
                    var producto = await _context.Productos.FindAsync(lineaDto.IdProducto);
                    if (producto == null)
                    {
                        throw new InvalidOperationException($"El producto con ID {lineaDto.IdProducto} no existe.");
                    }

                    producto.Stock += lineaDto.Cantidad;

                    var nuevaLinea = new LineaPedido
                    {
                        IdPedido = nuevoPedido.IdPedido,
                        NroLineaPedido = nroLinea++,
                        IdProducto = lineaDto.IdProducto,
                        Cantidad = lineaDto.Cantidad
                    };
                    _context.LineaPedidos.Add(nuevaLinea);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new PedidoDTO
                {
                    IdPedido = nuevoPedido.IdPedido,
                    Estado = nuevoPedido.Estado,
                    Fecha = nuevoPedido.Fecha,
                    IdProveedor = nuevoPedido.IdProveedor.GetValueOrDefault()
                };
            }
            catch (Exception)
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

                // Solo revertir stock si el pedido no fue recibido
                if (pedido.Estado == "Pendiente")
                {
                    foreach (var linea in pedido.LineasPedido)
                    {
                        var producto = await _context.Productos.FindAsync(linea.IdProducto);
                        if (producto != null)
                        {
                            producto.Stock -= linea.Cantidad;
                            if (producto.Stock < 0) producto.Stock = 0; // Evitar stock negativo
                        }
                    }
                }

                _context.Pedidos.Remove(pedido); // Esto eliminará en cascada las líneas de pedido
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

