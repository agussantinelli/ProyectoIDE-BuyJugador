using DominioModelo;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace DominioServicios
{
    public class LocalidadService
    {
        public void Add(Localidad localidad)
        {
            localidad.SetCodigoLocalidad(GetNextCodigo(localidad.CodigoProvincia));
            LocalidadInMemory.Localidades.Add(localidad);
        }

        public bool Delete(int codigoProvincia, int codigoLocalidad)
        {
            var loc = LocalidadInMemory.Localidades
                .Find(x => x.CodigoProvincia == codigoProvincia && x.CodigoLocalidad == codigoLocalidad);

            if (loc != null)
            {
                LocalidadInMemory.Localidades.Remove(loc);
                return true;
            }

            return false;
        }

        public Localidad? Get(int codigoProvincia, int codigoLocalidad)
        {
            return LocalidadInMemory.Localidades
                .Find(x => x.CodigoProvincia == codigoProvincia && x.CodigoLocalidad == codigoLocalidad);
        }

        public IEnumerable<Localidad> GetAll()
        {
            return LocalidadInMemory.Localidades.ToList();
        }

        public bool Update(Localidad localidad)
        {
            var existing = LocalidadInMemory.Localidades
                .Find(x => x.CodigoProvincia == localidad.CodigoProvincia && x.CodigoLocalidad == localidad.CodigoLocalidad);

            if (existing != null)
            {
                existing.SetNombreLocalidad(localidad.NombreLocalidad);
                return true;
            }

            return false;
        }

        private static int GetNextCodigo(int codigoProvincia)
        {
            var localidades = LocalidadInMemory.Localidades
                .Where(x => x.CodigoProvincia == codigoProvincia)
                .ToList();

            return localidades.Any() ? localidades.Max(x => x.CodigoLocalidad) + 1 : 1;
        }
    }
}

