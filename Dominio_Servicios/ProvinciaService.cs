using Data;
using DominioModelo;
namespace DominioServicios
{
    using DominioModelo;
    using Data;

    public class ProvinciaService
    {
        public IEnumerable<Provincia> GetAll() => ProvinciaInMemory.Provincias;

        public Provincia? GetByCodigo(int codigo) => ProvinciaInMemory.Provincias.FirstOrDefault(p => p.CodigoProvincia == codigo);

        public bool Add(Provincia provincia)
        {
            if (GetByCodigo(provincia.CodigoProvincia) != null)
            {
                return false;
            }
            ProvinciaInMemory.Provincias.Add(provincia);
            return true;
        }

        public bool Update(int codigo, Provincia provinciaActualizada)
        {
            var provinciaExistente = GetByCodigo(codigo);
            if (provinciaExistente == null)
            {
                return false;
            }
            ProvinciaInMemory.Provincias.Remove(provinciaExistente);

            ProvinciaInMemory.Provincias.Add(provinciaActualizada);
            return true;
        }

        public bool Delete(int codigo)
        {
            var provincia = GetByCodigo(codigo);
            if (provincia == null)
            {
                return false;
            }
            ProvinciaInMemory.Provincias.Remove(provincia);
            return true;
        }
    }
}