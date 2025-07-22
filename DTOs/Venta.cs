namespace DTO
{
    public class Venta
    {
        public int NroVenta { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Estado { get; private set; }
        public decimal MontoTotal { get; private set; }
        public int LegajoCliente { get; private set; }
    }
}