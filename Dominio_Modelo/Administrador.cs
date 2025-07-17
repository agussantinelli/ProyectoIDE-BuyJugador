namespace Domain.Model
{
	public class Administrador : Persona
	{
		public Administrador(int legajo, string nombre, string mail, string contrasenia)
			: base(legajo, nombre, mail, contrasenia)
		{
		}
	}
}