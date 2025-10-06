namespace DTOs
{
    // DTO especializado para llevar el producto junto a su precio de compra específico.
    public class ProductoAsignadoDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
    }
}
