using System;

namespace Dominio_Modelo
{
    public class TipoProducto
    {
        public int IdTipoProducto { get; private set; }
        public string NombreTipoProducto { get; private set; }

        public TipoProducto(int idTipoProducto, string nombreTipoProducto)
        {
            SetIdTipoProducto(idTipoProducto);
            SetNombreTipoProducto(nombreTipoProducto);
        }

        public void SetIdTipoProducto(int idTipoProducto)
        {
            if (idTipoProducto <= 0)
                throw new ArgumentException("El código del tipo de producto debe ser positivo.", nameof(idTipoProducto));
            IdTipoProducto = idTipoProducto;
        }

        public void SetNombreTipoProducto(string nombreTipoProducto)
        {
            if (string.IsNullOrWhiteSpace(nombreTipoProducto))
                throw new ArgumentException("El nombre del tipo de producto no puede ser nulo o vacío.", nameof(nombreTipoProducto));
            NombreTipoProducto = nombreTipoProducto;
        }
    }
}