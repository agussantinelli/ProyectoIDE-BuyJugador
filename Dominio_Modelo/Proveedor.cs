using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioModelo
{
    public class Proveedor
    {
        public string Cuil { get; private set; }
        public string RazonSocial { get; private set; }
        public string TelefonoProveedor { get; private set; }
        public string MailProveedor { get; private set; }

        public Proveedor(string cuil, string razonSocial, string telefonoProveedor, string mailProveedor)
        {
            SetCuil(cuil);
            SetRazonSocial(razonSocial);
            SetTelefonoProveedor(telefonoProveedor);
            SetMailProveedor(mailProveedor);
        }

        public void SetCuil(string cuil)
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

        public void SetTelefonoProveedor(string telefonoProveedor)
        {
            if (string.IsNullOrWhiteSpace(telefonoProveedor))
                throw new ArgumentException("El teléfono no puede ser nulo o vacío.", nameof(telefonoProveedor));
            TelefonoProveedor = telefonoProveedor;
        }

        public void SetMailProveedor(string mailProveedor)
        {
            if (string.IsNullOrWhiteSpace(mailProveedor))
                throw new ArgumentException("El mail no puede ser nulo o vacío.", nameof(mailProveedor));
            MailProveedor = mailProveedor;
        }
    }
}
