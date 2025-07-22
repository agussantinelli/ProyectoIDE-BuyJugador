using Dominio_Modelo;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace Dominio_Servicios
{
    public class ProvinciaService
    {
        public bool Add(Provincia provincia)
        {
            if (ProvinciaInMemory.Provincias.Any(p => p.NombreProvincia.Equals(provincia.NombreProvincia, System.StringComparison.OrdinalIgnoreCase)))
                return false;

            provincia.SetCodigoProvincia(GetNextCodigoProvincia());
            ProvinciaInMemory.Provincias.Add(provincia);
            return true;
        }

        public bool Delete(int codigoProvincia)
        {
            var provinciaToDelete = ProvinciaInMemory.Provincias.FirstOrDefault(x => x.CodigoProvincia == codigoProvincia);
            if (provinciaToDelete != null)
            {
                ProvinciaInMemory.Provincias.Remove(provinciaToDelete);
                return true;
            }
            return false;
        }

        public Provincia? Get(int codigoProvincia)
        {
            return ProvinciaInMemory.Provincias.FirstOrDefault(x => x.CodigoProvincia == codigoProvincia);
        }

        public IReadOnlyList<Provincia> GetAll()
        {
            return ProvinciaInMemory.Provincias.AsReadOnly();
        }

        public bool Update(Provincia provincia)
        {
            var provinciaToUpdate = ProvinciaInMemory.Provincias.FirstOrDefault(x => x.CodigoProvincia == provincia.CodigoProvincia);
            if (provinciaToUpdate != null)
            {
                provinciaToUpdate.SetNombreProvincia(provincia.NombreProvincia);
                return true;
            }
            return false;
        }

        private static int GetNextCodigoProvincia()
        {
            return ProvinciaInMemory.Provincias.Any() ? ProvinciaInMemory.Provincias.Max(x => x.CodigoProvincia) + 1 : 1;
        }
    }
}
