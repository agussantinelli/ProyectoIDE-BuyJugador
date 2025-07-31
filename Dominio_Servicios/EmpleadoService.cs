using DominioModelo;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace DominioServicios
{
    public class EmpleadoService
    {
        public bool Add(Empleado empleado)
        {
            if (EmpleadoInMemory.Empleados.Any(e => e.Dni == empleado.Dni))
                return false; // 

            EmpleadoInMemory.Empleados.Add(empleado);
            return true;
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
