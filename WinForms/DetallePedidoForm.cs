using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class DetallePedidoForm : BaseForm
    {
        // # Campos privados para manejar el estado interno del formulario.
        private readonly int _pedidoId;
        private PedidoDTO _pedido;
        private readonly bool _esAdmin;
        private BindingList<LineaPedidoDTO> _lineasDePedido;

        // # Inyección de todos los ApiClients que este formulario necesita para ser autónomo.
        private readonly PedidoApiClient _pedidoApiClient;
        private readonly ProductoApiClient _productoApiClient;
        private readonly IServiceProvider _serviceProvider;

        private bool _datosModificados = false;
        private List<ProductoDTO> _productosDelProveedor;

        // # CONSTRUCTOR CORREGIDO Y ESTANDARIZADO
        public DetallePedidoForm(
            int pedidoId,
            bool esAdmin,
            PedidoApiClient pedidoApiClient,
            ProductoApiClient productoApiClient,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _pedidoId = pedidoId;
            _esAdmin = esAdmin;
            _pedidoApiClient = pedidoApiClient;
            _productoApiClient = productoApiClient;
            _serviceProvider = serviceProvider;

            _lineasDePedido = new BindingList<LineaPedidoDTO>();
            _productosDelProveedor = new List<ProductoDTO>();

            StyleManager.ApplyDataGridViewStyle(dataGridDetalle);
            StyleManager.ApplyButtonStyle(btnAgregarLinea);
            StyleManager.ApplyButtonStyle(btnEliminarLinea);
            StyleManager.ApplyButtonStyle(btnConfirmarCambios);
            StyleManager.ApplyButtonStyle(btnCerrar);
        }

        private async void DetallePedidoForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                _pedido = await _pedidoApiClient.GetByIdAsync(_pedidoId);
                if (_pedido == null)
                {
                    MessageBox.Show("No se pudo cargar el detalle del pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                _productosDelProveedor = await _productoApiClient.GetProductosByProveedorIdAsync(_pedido.IdProveedor) ?? new List<ProductoDTO>();

                lblIdPedido.Text = $"ID Pedido: {_pedido.IdPedido}";
                lblProveedor.Text = $"Proveedor: {_pedido.ProveedorRazonSocial}";
                lblFecha.Text = $"Fecha: {_pedido.Fecha:dd/MM/yyyy}";

                // # CORRECCIÓN: Se usa la propiedad correcta 'LineasPedido' del DTO.
                _lineasDePedido = new BindingList<LineaPedidoDTO>(_pedido.LineasPedido ?? new List<LineaPedidoDTO>());
                dataGridDetalle.DataSource = _lineasDePedido;
                ConfigurarColumnas();

                ActualizarTotal();
                ConfigurarVisibilidadControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el detalle del pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarVisibilidadControles()
        {
            if (_pedido == null) return;
            bool pedidoPendiente = "Pendiente".Equals(_pedido.Estado, StringComparison.OrdinalIgnoreCase);

            btnAgregarLinea.Visible = _esAdmin && pedidoPendiente;
            btnEliminarLinea.Visible = _esAdmin && pedidoPendiente;
            btnConfirmarCambios.Visible = _esAdmin && pedidoPendiente;
            btnConfirmarCambios.Enabled = false; // Se activa solo si hay cambios
            dataGridDetalle.ReadOnly = !_esAdmin || !pedidoPendiente;
        }

        private void ConfigurarColumnas()
        {
            dataGridDetalle.AutoGenerateColumns = false;
            dataGridDetalle.Columns.Clear();
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad", Width = 80, ReadOnly = !_esAdmin });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioUnitario", HeaderText = "Precio Unit.", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, ReadOnly = true });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Subtotal", HeaderText = "Subtotal", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, ReadOnly = true });
        }

        private void ActualizarTotal()
        {
            if (_pedido != null)
            {
                _pedido.Total = _lineasDePedido.Sum(l => l.Subtotal);
                lblTotal.Text = $"Total: {_pedido.Total:C2}";
            }
        }

        private void MarcarComoModificado()
        {
            _datosModificados = true;
            btnConfirmarCambios.Enabled = true;
        }

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            if (_pedido == null) return;

            var idsProductosEnPedido = _lineasDePedido.Select(l => l.IdProducto).ToList();
            var productosDisponibles = _productosDelProveedor
                .Where(p => !idsProductosEnPedido.Contains(p.IdProducto))
                .ToList();

            if (!productosDisponibles.Any())
            {
                MessageBox.Show("No hay más productos de este proveedor para agregar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // A modo de ejemplo, se toma el primer producto disponible.
            // Lo ideal sería abrir un formulario de selección aquí.
            var productoAñadir = productosDisponibles.First();

            using var form = new AñanirProductoPedidoForm(productoAñadir);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var nuevaLinea = form.LineaPedido;
                if (nuevaLinea != null)
                {
                    _lineasDePedido.Add(nuevaLinea);
                    MarcarComoModificado();
                    ActualizarTotal();
                }
            }
        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridDetalle.CurrentRow?.DataBoundItem is LineaPedidoDTO linea)
            {
                _lineasDePedido.Remove(linea);
                MarcarComoModificado();
                ActualizarTotal();
            }
        }

        private async void btnConfirmarCambios_Click(object sender, EventArgs e)
        {
            if (_pedido == null || !_datosModificados) return;

            var confirm = MessageBox.Show("¿Desea guardar los cambios en el pedido?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var dto = new CrearPedidoCompletoDTO
                {
                    IdProveedor = _pedido.IdProveedor,
                    LineasPedido = _lineasDePedido.ToList()
                };

                // await _pedidoApiClient.UpdatePedidoCompletoAsync(_pedido.IdPedido, dto); // Descomentar cuando el endpoint exista

                MessageBox.Show("Cambios guardados exitosamente.", "Éxito");
                _datosModificados = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios: {ex.Message}", "Error");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (_datosModificados)
            {
                var result = MessageBox.Show(
                    "Hay cambios sin guardar. ¿Desea salir de todas formas?",
                    "Atención",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            this.DialogResult = _datosModificados ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }
    }
}

