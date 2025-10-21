using Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Data
{
    // #NUEVO: Clase de Unidad de Trabajo (Unit of Work).
    // #Intención: Centralizar el acceso a todos los repositorios y gestionar las transacciones
    // #de la base de datos a través de una única instancia del DbContext.
    public class UnitOfWork : IDisposable
    {
        private readonly BuyJugadorContext _context;
        private bool _disposed = false;

        // #Intención: Exponer una única instancia de cada repositorio.
        // #Lógica: Se utiliza "lazy instantiation" para crear los repositorios solo cuando se necesitan.
        public LineaPedidoRepository LineaPedidoRepository { get; private set; }
        public LineaVentaRepository LineaVentaRepository { get; private set; }
        public LocalidadRepository LocalidadRepository { get; private set; }
        public PedidoRepository PedidoRepository { get; private set; }
        public PersonaRepository PersonaRepository { get; private set; }
        public PrecioCompraRepository PrecioCompraRepository { get; private set; }
        public PrecioVentaRepository PrecioVentaRepository { get; private set; }
        public ProductoRepository ProductoRepository { get; private set; }
        public ProductoProveedorRepository ProductoProveedorRepository { get; private set; }
        public ProveedorRepository ProveedorRepository { get; private set; }
        public ProvinciaRepository ProvinciaRepository { get; private set; }
        public ReporteRepository ReporteRepository { get; private set; }
        public TipoProductoRepository TipoProductoRepository { get; private set; }
        public VentaRepository VentaRepository { get; private set; }

        public UnitOfWork(BuyJugadorContext context)
        {
            _context = context;
            // #Lógica: Se instancian todos los repositorios, compartiendo el mismo DbContext.
            // #Esto es crucial para que las transacciones funcionen correctamente.
            LineaPedidoRepository = new LineaPedidoRepository(_context);
            LineaVentaRepository = new LineaVentaRepository(_context);
            LocalidadRepository = new LocalidadRepository(_context);
            PedidoRepository = new PedidoRepository(_context);
            PersonaRepository = new PersonaRepository(_context);
            PrecioCompraRepository = new PrecioCompraRepository(_context);
            PrecioVentaRepository = new PrecioVentaRepository(_context);
            ProductoRepository = new ProductoRepository(_context);
            ProductoProveedorRepository = new ProductoProveedorRepository(_context);
            ProveedorRepository = new ProveedorRepository(_context);
            ProvinciaRepository = new ProvinciaRepository(_context);
            ReporteRepository = new ReporteRepository(_context);
            TipoProductoRepository = new TipoProductoRepository(_context);
            VentaRepository = new VentaRepository(_context);
        }

        // #Intención: Confirmar todos los cambios pendientes en la base de datos
        // #como una única transacción.
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // #Intención: Liberar la conexión y los recursos del DbContext.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
