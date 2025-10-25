using Data;
using DominioModelo;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class PedidoService
    {
        private readonly UnitOfWork _unitOfWork;

        public PedidoService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private DateTime GetCurrentArgentinaTime()
        {
            try
            {
                string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? "Argentina Standard Time"
                    : "America/Argentina/Buenos_Aires";
                TimeZoneInfo argentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, argentinaTimeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                return DateTime.UtcNow.AddHours(-3);
            }
        }

        public async Task<List<PedidoDTO>> GetAllPedidosDetalladosAsync()
        {
            var pedidos = await _unitOfWork.PedidoRepository.GetAllPedidosDetalladosAsync();

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
            var pedido = await _unitOfWork.PedidoRepository.GetPedidoDetalladoByIdAsync(id);
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
            var nuevoPedido = new Pedido
            {
                Fecha = GetCurrentArgentinaTime(),
                Estado = crearPedidoDto.MarcarComoRecibido ? "Recibido" : "Pendiente",
                IdProveedor = crearPedidoDto.IdProveedor
            };
            await _unitOfWork.PedidoRepository.AddAsync(nuevoPedido);
            await _unitOfWork.SaveChangesAsync();

            var idsProductos = crearPedidoDto.LineasPedido.Select(l => l.IdProducto).Distinct().ToList();
            var productosAfectados = (await _unitOfWork.ProductoRepository.GetByIdsAsync(idsProductos))
                .ToDictionary(p => p.IdProducto);

            int nroLinea = 1;
            foreach (var lineaDto in crearPedidoDto.LineasPedido)
            {
                if (!productosAfectados.TryGetValue(lineaDto.IdProducto, out var producto))
                {
                    throw new InvalidOperationException($"Producto ID {lineaDto.IdProducto} no encontrado o inactivo.");
                }

                var montoPrecioCompra = await _unitOfWork.PrecioCompraRepository.GetMontoAsync(lineaDto.IdProducto, crearPedidoDto.IdProveedor);
                if (!montoPrecioCompra.HasValue)
                {
                    throw new InvalidOperationException($"Precio de compra no encontrado para el producto ID {lineaDto.IdProducto} y proveedor ID {crearPedidoDto.IdProveedor}.");
                }

                await _unitOfWork.PedidoRepository.AddLineaAsync(new LineaPedido
                {
                    IdPedido = nuevoPedido.IdPedido,
                    NroLineaPedido = nroLinea++,
                    IdProducto = lineaDto.IdProducto,
                    Cantidad = lineaDto.Cantidad,
                    PrecioUnitario = montoPrecioCompra.Value
                });

                if (crearPedidoDto.MarcarComoRecibido)
                {
                    producto.Stock += lineaDto.Cantidad;
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return await GetPedidoDetalladoByIdAsync(nuevoPedido.IdPedido);
        }

        public async Task UpdatePedidoCompletoAsync(int id, PedidoDTO pedidoDto)
        {
            var pedido = await _unitOfWork.PedidoRepository.GetByIdAsync(id);
            if (pedido == null) throw new KeyNotFoundException("Pedido no encontrado.");
            if (pedido.Estado != "Pendiente") throw new InvalidOperationException("Solo se pueden modificar pedidos en estado 'Pendiente'.");

            _unitOfWork.PedidoRepository.RemoveLineas(pedido.LineasPedido);

            var idsProductosNuevos = pedidoDto.LineasPedido.Select(l => l.IdProducto).ToList();
            var productosNuevos = (await _unitOfWork.ProductoRepository.GetByIdsAsync(idsProductosNuevos))
                                    .ToDictionary(p => p.IdProducto);

            int nroLinea = 1;
            foreach (var lineaDto in pedidoDto.LineasPedido)
            {
                if (productosNuevos.ContainsKey(lineaDto.IdProducto))
                {
                    await _unitOfWork.PedidoRepository.AddLineaAsync(new LineaPedido
                    {
                        IdPedido = pedido.IdPedido,
                        NroLineaPedido = nroLinea++,
                        IdProducto = lineaDto.IdProducto,
                        Cantidad = lineaDto.Cantidad,
                        PrecioUnitario = lineaDto.PrecioUnitario
                    });
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task MarcarComoRecibidoAsync(int id)
        {
            var pedido = await _unitOfWork.PedidoRepository.GetByIdAsync(id);
            if (pedido == null) throw new KeyNotFoundException("Pedido no encontrado.");
            if (pedido.Estado == "Recibido") throw new InvalidOperationException("El pedido ya fue recibido.");

            var idsProductos = pedido.LineasPedido.Select(l => l.IdProducto).ToList();
            var productosAfectados = (await _unitOfWork.ProductoRepository.GetByIdsAsync(idsProductos))
                                        .ToDictionary(p => p.IdProducto);

            foreach (var linea in pedido.LineasPedido)
            {
                if (productosAfectados.TryGetValue(linea.IdProducto, out var producto))
                {
                    producto.Stock += linea.Cantidad;
                }
            }

            pedido.Estado = "Recibido";
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePedidoCompletoAsync(int id)
        {
            var pedido = await _unitOfWork.PedidoRepository.GetByIdAsync(id);
            if (pedido == null) throw new KeyNotFoundException("Pedido no encontrado.");

            if (pedido.Estado == "Recibido")
            {
                var idsProductos = pedido.LineasPedido.Select(l => l.IdProducto).ToList();
                var productosAfectados = (await _unitOfWork.ProductoRepository.GetByIdsAsync(idsProductos))
                                       .ToDictionary(p => p.IdProducto);

                foreach (var linea in pedido.LineasPedido)
                {
                    if (productosAfectados.TryGetValue(linea.IdProducto, out var producto))
                    {
                        producto.Stock -= linea.Cantidad;
                        if (producto.Stock < 0) producto.Stock = 0;
                    }
                }
            }
            _unitOfWork.PedidoRepository.Remove(pedido);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> GetCantidadPedidosPendientesAsync()
        {
            return await _unitOfWork.PedidoRepository.GetCantidadPedidosPendientesAsync();
        }
    }
}
