using System;

namespace Dominio_Modelo
{
    public class TipoProducto
    {
        public int CodTipoProducto { get; private set; }
        public string Nombre { get; private set; }

        public TipoProducto(int codTipoProducto, string nombre)
        {
            SetCodTipoProducto(codTipoProducto);
            SetNombre(nombre);
        }

        public void SetCodTipoProducto(int codTipoProducto)
        {
            if (codTipoProducto <= 0)
                throw new ArgumentException("El código del tipo de producto debe ser positivo.", nameof(codTipoProducto));
            CodTipoProducto = codTipoProducto;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del tipo de producto no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }
    }
}