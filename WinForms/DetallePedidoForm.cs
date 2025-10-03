using ApiClient;
using DTOs;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class DetallePedidoForm : BaseForm
    {
        private readonly LineaPedidoApiClient _lineaPedidoApiClient;
        private readonly PedidoApiClient _pedidoApiClient;
        private BindingList<LineaPedidoDTO> _lineasPedidoBindingList;

        public PedidoDTO Pedido { get; set; }
        public bool EsAdmin { get; set; }

        public DetallePedidoForm(LineaPedidoApiClient lineaPedidoApiClient, PedidoApiClient pedidoApiClient)
        {
            InitializeComponent();
            _lineaPedidoApiClient = lineaPedidoApiClient;
            _pedidoApiClient = pedidoApiClient;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Estilos
            StyleManager.ApplyDataGridViewStyle(dataGridDetalles);
            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
            StyleManager.ApplyButtonStyle(btnEliminar);
        }

        private async void DetallePedidoForm_Load(object sender, EventArgs e)
        {
            if (Pedido == null)
            {
                MessageBox.Show("No se ha proporcionado un pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblNumeroPedido.Text = $"Detalles del Pedido #{Pedido.IdPedido}";
            lblProveedor.Text = $"Proveedor: {Pedido.ProveedorRazonSocial}";
            lblFecha.Text = $"Fecha: {Pedido.Fecha:dd/MM/yyyy}";
            lblEstado.Text = $"Estado: {Pedido.Estado}";

            await CargarLineasPedido();
            ConfigurarControles();
        }

        private async Task CargarLineasPedido()
        {
            try
            {
                var lineas = await _lineaPedidoApiClient.GetLineasByPedidoIdAsync(Pedido.IdPedido);
                _lineasPedidoBindingList = new BindingList<LineaPedidoDTO>(lineas);
                dataGridDetalles.DataSource = _lineasPedidoBindingList;
                ActualizarTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los detalles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarControles()
        {
            bool editable = EsAdmin && Pedido.Estado == "Pendiente";
            dataGridDetalles.ReadOnly = !editable;
            btnGuardar.Visible = editable;
            btnEliminar.Visible = editable;
            btnCancelar.Text = editable ? "Cancelar Cambios" : "Cerrar";
        }

        private void dataGridDetalles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (dataGridDetalles.Columns[e.ColumnIndex].Name == "Cantidad" || dataGridDetalles.Columns[e.ColumnIndex].Name == "PrecioUnitario"))
            {
                ActualizarTotal();
            }
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = $"Total: {_lineasPedidoBindingList.Sum(l => l.Subtotal):C}";
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Pedido.LineasPedido = _lineasPedidoBindingList.ToList();
                var response = await _pedidoApiClient.UpdateAsync(Pedido.IdPedido, Pedido);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Pedido actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Error al actualizar el pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridDetalles.CurrentRow?.DataBoundItem is LineaPedidoDTO linea)
            {
                _lineasPedidoBindingList.Remove(linea);
                ActualizarTotal();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
