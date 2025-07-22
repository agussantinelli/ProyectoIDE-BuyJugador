using Dominio_Modelo;

namespace Domain.Model
{
	public class Duenio : Persona
	{
		public Duenio(int dni, string nombrePer, string mailPer, string contrasenia, string telefonoPer)
            : base(dni,nombrePer,mailPer,contrasenia,telefonoPer)

        {
		}
	}
}