namespace DominioModelo
{
    public class Empleado : Persona
    {
        public int IdEmpleado { get; set; }

        public Empleado(int idEmpleado, string nombre, int cuit, string email, int telefono, string direccion, int idLocalidad) : base(nombre, cuit, email, telefono, direccion, idLocalidad)
        {
            IdEmpleado = idEmpleado;
        }
    }
}
