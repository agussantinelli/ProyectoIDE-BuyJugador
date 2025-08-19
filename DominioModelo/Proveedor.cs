namespace DominioModelo
{
    public class Proveedor : Persona
    {
        public int IdProveedor { get; set; }

        public Proveedor(int idProveedor, string nombre, int cuit, string email, int telefono, string direccion, int idLocalidad) : base(nombre, cuit, email, telefono, direccion, idLocalidad)
        {
            IdProveedor = idProveedor;
        }
    }
}
