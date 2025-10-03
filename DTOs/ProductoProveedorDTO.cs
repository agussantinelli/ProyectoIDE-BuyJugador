using System.Collections.Generic;

namespace DTOs
{
    public class ProductoProveedorDTO
    {
        public int IdProveedor { get; set; }
        public List<int> IdsProducto { get; set; } = new List<int>();
    }
}
