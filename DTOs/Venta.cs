namespace DTOs
{
    public class VentaDto
    {
        public int IdVenta { get; private set; }
        public DateTime FechaVenta { get; private set; }
        public string EstadoVenta { get; private set; }
        public decimal MontoTotalVenta { get; private set; }
        public int DniVendedor { get; private set; }
    }
}