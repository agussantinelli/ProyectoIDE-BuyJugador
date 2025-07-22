using Dominio_Modelo;
using Data;

namespace Dominio_Servicios
{
    public class DuenioService
    {
        public void Add(Duenio duenio)
        {
            DuenioInMemory.Duenios.Add(duenio);
        }

        public bool Delete(int dni)
        {
            var obj = DuenioInMemory.Duenios.Find(x => x.Dni == dni);
            if (obj != null)
            {
                DuenioInMemory.Duenios.Remove(obj);
                return true;
            }
            return false;
        }

        public Duenio? Get(int dni)
        {
            return DuenioInMemory.Duenios.Find(x => x.Dni == dni);
        }

        public IEnumerable<Duenio> GetAll()
        {
            return DuenioInMemory.Duenios.ToList();
        }

        public bool Update(Duenio duenio)
        {
            var obj = DuenioInMemory.Duenios.Find(x => x.Dni == duenio.Dni);
            if (obj != null)
            {
                obj.SetNombrePersona(duenio.NombrePersona);
                obj.SetMailPersona(duenio.MailPersona);
                obj.SetContrasenia(duenio.Contrasenia);
                obj.SetTelefonoPersona(duenio.TelefonoPersona);
                return true;
            }
            return false;
        }
    }
}

