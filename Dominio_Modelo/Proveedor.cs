using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Modelo
{
    public class Proveedor
    {
        public string Cuil { get; private set; }
        public string RazonSocial { get; private set; }
        public string TelefonoProve { get; private set; }
        public string MailProve { get; private set; }

        public Proveedor(string cuil, string razonSocial, string telefonoProve, string mailProve)
        {
            SetCuit(cuil);
            SetRazonSocial(razonSocial);
            SetTelefonoProve(telefonoProve);
            SetMailProve(mailProve);
        }

        public void SetCuit(string cuil)
        {
            if (string.IsNullOrWhiteSpace(cuil))
                throw new ArgumentException("El CUIT no puede ser nulo o vacío.", nameof(cuil));
            Cuil = cuil;
        }

        public void SetRazonSocial(string razonSocial)
        {
            if (string.IsNullOrWhiteSpace(razonSocial))
                throw new ArgumentException("La razón social no puede ser nula o vacía.", nameof(razonSocial));
            RazonSocial = razonSocial;
        }

        public void SetTelefonoProve(string telefonoProve)
        {
            if (string.IsNullOrWhiteSpace(telefonoProve))
                throw new ArgumentException("El teléfono no puede ser nulo o vacío.", nameof(telefonoProve));
            TelefonoProve = telefonoProve;
        }

        public void SetMailProve(string mailProve)
        {
            if (string.IsNullOrWhiteSpace(mailProve))
                throw new ArgumentException("El mail no puede ser nulo o vacío.", nameof(mailProve));
            MailProve = mailProve;
        }
    }
}
