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
        public List<PrecioDTO> Precios { get; set; } = new List<PrecioDTO>();

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
                // Corrección: Usa la propiedad de navegación correcta
                TipoProductoDescripcion = entidad.IdTipoProductoNavigation?.Descripcion,
                PrecioActual = entidad.Precios?.OrderByDescending(p => p.FechaDesde).FirstOrDefault()?.Monto ?? 0,
                Precios = entidad.Precios?.Select(PrecioDTO.FromDominio).ToList() ?? new List<PrecioDTO>()
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
                Activo = true // Asumimos que al crearlo/editarlo siempre está activo
            };

            if (this.Precios != null)
            {
                foreach (var precioDto in this.Precios)
                {
                    // Se añade una comprobación para evitar el error si un precio en la lista es nulo
                    if (precioDto != null)
                    {
                        // Mapeo manual para mayor seguridad
                        productoDominio.Precios.Add(new DominioModelo.Precio
                        {
                            Monto = precioDto.Monto,
                            FechaDesde = precioDto.FechaDesde
                            // EF Core se encargará del IdProducto al guardar la relación
                        });
                    }
                }
            }

            return productoDominio;
        }
    }
}

