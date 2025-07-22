using Dominio_Modelo;
using Data;

namespace Dominio_Servicios
{
    public class EmpleadoService
    {
        public void Add(Empleado empleado)
        {
            EmpleadoInMemory.Empleados.Add(empleado);
        }

        public bool Delete(int dni)
        {
            var obj = EmpleadoInMemory.Empleados.Find(x => x.Dni == dni);
            if (obj != null)
            {
                EmpleadoInMemory.Empleados.Remove(obj);
                return true;
            }
            return false;
        }

        public Empleado? Get(int dni)
        {
            return EmpleadoInMemory.Empleados.Find(x => x.Dni == dni);
        }

        public IEnumerable<Empleado> GetAll()
        {
            return EmpleadoInMemory.Empleados.ToList();
        }

        public bool Update(Empleado empleado)
        {
            var obj = EmpleadoInMemory.Empleados.Find(x => x.Dni == empleado.Dni);
            if (obj != null)
            {
                obj.SetNombrePersona(empleado.NombrePersona);
                obj.SetMailPersona(empleado.MailPersona);
                obj.SetContrasenia(empleado.Contrasenia);
                obj.SetTelefonoPersona(empleado.TelefonoPersona);
                obj.SetFechaIngreso(empleado.FechaIngreso);
                return true;
            }
            return false;
        }
    }
}

