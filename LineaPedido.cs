namespace Dominio_Modelo
{
    public class LineaPedido
    {
        public int IdPedido { get; private set; }
        public decimal NroLineaPedido { get; private set; }
        public int IdProducto { get; private set; }
        public int CantidadPedido { get; private set; }

        public LineaPedido(int idPedido, decimal nroLineaPedido, int idProducto, int cantidadPedido)
        {
            SetIdPedido(idPedido);
            SetNroLineaPedido(nroLineaPedido);
            SetIdProducto(idProducto);
            SetCantidadPedido(cantidadPedido);
        }

        public void SetIdPedido(int idPedido)
        {
            if (idPedido <= 0)
                throw new ArgumentException("El ID de pedido debe ser positivo.", nameof(idPedido));
            IdPedido = idPedido;
        }

        public void SetNroLineaPedido(decimal nroLineaPedido)
        {
            if (nroLineaPedido <= 0)
                throw new ArgumentException("El número de línea de pedido debe ser positivo.", nameof(nroLineaPedido));
            NroLineaPedido = nroLineaPedido;
        }

        public void SetIdProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID de producto debe ser positivo.", nameof(idProducto));
            IdProducto = idProducto;
        }

        public void SetCantidadPedido(int cantidadPedido)
        {
            if (cantidadPedido <= 0)
                throw new ArgumentException("La cantidad de pedido debe ser positiva.", nameof(cantidadPedido));
            CantidadPedido = cantidadPedido;
        }
    }
}
