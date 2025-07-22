using Dominio_Modelo;
using Data;

namespace Dominio_Servicios
{
    public class EmpleadoService
    {
        public void Add(Empleado empleado)
        {
            empleado.SetId(GetNextId());
            EmpleadoInMemory.Empleados.Add(empleado);
        }

        public bool Delete(int id)
        {
            var obj = EmpleadoInMemory.Empleados.Find(x => x.Id == id);
            if (obj != null)
            {
                EmpleadoInMemory.Empleados.Remove(obj);
                return true;
            }
            return false;
        }

        public Empleado? Get(int id)
        {
            return EmpleadoInMemory.Empleados.Find(x => x.Id == id);
        }

        public IEnumerable<Empleado> GetAll()
        {
            return EmpleadoInMemory.Empleados.ToList();
        }

        public bool Update(Empleado empleado)
        {
            var obj = EmpleadoInMemory.Empleados.Find(x => x.Id == empleado.Id);
            if (obj != null)
            {
                obj.SetDni(empleado.Dni);
                obj.SetNombrePersona(empleado.NombrePersona);
                obj.SetMailPersona(empleado.MailPersona);
                obj.SetContrasenia(empleado.Contrasenia);
                obj.SetTelefonoPersona(empleado.TelefonoPersona);
                obj.SetFechaIngreso(empleado.FechaIngreso);
                return true;
            }
            return false;
        }

        private static int GetNextId()
        {
            return EmpleadoInMemory.Empleados.Count > 0 ? EmpleadoInMemory.Empleados.Max(x => x.Id) + 1 : 1;
        }
    }
}
