using Dominio_Modelo;
using Data;
namespace Dominio_Servicios
{
    public class ProvinciaService
    {
        public void Add(Provincia provincia)
        {
            provincia.SetCodigoProvincia(GetNextCodigoProvincia());
            ProvinciaInMemory.Provincias.Add(provincia);
        }

        public bool Delete(int codigoProvincia)
        {
            var provinciaToDelete = ProvinciaInMemory.Provincias.Find(x => x.CodigoProvincia == codigoProvincia);
            if (provinciaToDelete != null)
            {
                ProvinciaInMemory.Provincias.Remove(provinciaToDelete);
                return true;
            }
            return false;
        }

        public Provincia Get(int codigoProvincia)
        {
            return ProvinciaInMemory.Provincias.Find(x => x.CodigoProvincia == codigoProvincia);
        }

        public IEnumerable<Provincia> GetAll()
        {
            return ProvinciaInMemory.Provincias.ToList();
        }

        public bool Update(Provincia provincia)
        {
            var provinciaToUpdate = ProvinciaInMemory.Provincias.Find(x => x.CodigoProvincia == provincia.CodigoProvincia);
            if (provinciaToUpdate != null)
            {
                provinciaToUpdate.SetNombreProvincia(provincia.NombreProvincia);
                return true;
            }
            return false;
        }

        private static int GetNextCodigoProvincia()
        {
            return ProvinciaInMemory.Provincias.Count > 0 ? ProvinciaInMemory.Provincias.Max(x => x.CodigoProvincia) + 1 : 1;
        }
    }
}