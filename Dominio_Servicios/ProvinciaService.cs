using Data;
using DominioModelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DominioServicio
{
    public class ProvinciaService
    {
        public void Add(Provincia provincia)
        {
            provincia.SetCodigoProvincia(GetNextCodigo());
            ProvinciaInMemory.Provincias.Add(provincia);
        }

        public bool Delete(int codigo)
        {
            var provinciaToDelete = ProvinciaInMemory.Provincias.Find(p => p.CodigoProvincia == codigo);
            if (provinciaToDelete != null)
            {
                ProvinciaInMemory.Provincias.Remove(provinciaToDelete);
                return true;
            }
            return false;
        }

        public Provincia Get(int codigo)
        {
            return ProvinciaInMemory.Provincias.FirstOrDefault(p => p.CodigoProvincia == codigo);
        }

        public IEnumerable<Provincia> GetAll()
        {
            return ProvinciaInMemory.Provincias.ToList();
        }

        public bool Update(Provincia provincia)
        {
            var provinciaToUpdate = ProvinciaInMemory.Provincias.Find(p => p.CodigoProvincia == provincia.CodigoProvincia);
            if (provinciaToUpdate != null)
            {
                provinciaToUpdate.SetNombreProvincia(provincia.NombreProvincia);
                return true;
            }
            return false;
        }

        private static int GetNextCodigo()
        {
            return ProvinciaInMemory.Provincias.Count > 0 ? ProvinciaInMemory.Provincias.Max(p => p.CodigoProvincia) + 1 : 1;
        }
    }
}