using System;
using System.Collections.Generic;
using System.Linq;

namespace DTOs
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int? IdTipoProducto { get; set; }
        public string TipoProductoDescripcion { get; set; }
        public decimal PrecioActual { get; set; }
        public List<PrecioVentaDTO> Precios { get; set; } = new List<PrecioVentaDTO>();

        public static ProductoDTO FromDominio(DominioModelo.Producto entidad)
        {
            if (entidad == null) return null;

            return new ProductoDTO
            {
                IdProducto = entidad.IdProducto,
                Nombre = entidad.Nombre,
                Descripcion = entidad.Descripcion,
                Stock = entidad.Stock,
                IdTipoProducto = entidad.IdTipoProducto,
                TipoProductoDescripcion = entidad.IdTipoProductoNavigation?.Descripcion,
                PrecioActual = entidad.Precios?.OrderByDescending(p => p.FechaDesde).FirstOrDefault()?.Monto ?? 0,
                Precios = entidad.Precios?.Select(PrecioVentaDTO.FromDominio).ToList() ?? new List<PrecioVentaDTO>()
            };
        }

        public DominioModelo.Producto ToDominio()
        {
            var productoDominio = new DominioModelo.Producto
            {
                IdProducto = this.IdProducto,
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                Stock = this.Stock,
                IdTipoProducto = this.IdTipoProducto,
                Activo = true 
            };

            if (this.Precios != null)
            {
                foreach (var precioDto in this.Precios)
                {
                    if (precioDto != null)
                    {
                        productoDominio.Precios.Add(new DominioModelo.PrecioVenta
                        {
                            Monto = precioDto.Monto,
                            FechaDesde = precioDto.FechaDesde
                        });
                    }
                }
            }

            return productoDominio;
        }
    }
}

