namespace DTOs
{
    // #NUEVO: DTO optimizado para listas de selección (select lists).
    // #Intención: Transferir solo los datos estrictamente necesarios (ID y Nombre),
    // #mejorando el rendimiento y reduciendo la carga de red.
    public class PersonaSimpleDTO
    {
        public int IdPersona { get; set; }
        public string? NombreCompleto { get; set; }
    }
}

