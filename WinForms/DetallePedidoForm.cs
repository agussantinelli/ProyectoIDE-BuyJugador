using ApiClient;
using DTOs;
using System;
using System.ComponentModel;
using System.Linq;
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


            var pedidoDetallado = await _pedidoApiClient.GetByIdAsync(Pedido.IdPedido);
            if (pedidoDetallado == null)
            {
                MessageBox.Show("No se pudo cargar el detalle del pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            Pedido = pedidoDetallado;


            lblNumeroPedido.Text = $"Pedido N°: {Pedido.IdPedido}";
            lblFecha.Text = $"Fecha: {Pedido.Fecha:dd/MM/yyyy}";
            lblProveedor.Text = $"Proveedor: {Pedido.ProveedorRazonSocial}";
            lblEstado.Text = $"Estado: {Pedido.Estado}";


            _lineasPedidoBindingList = new BindingList<LineaPedidoDTO>(Pedido.LineasPedido);
            dataGridDetalles.DataSource = _lineasPedidoBindingList;
            ConfigurarColumnas();

            bool esEditable = Pedido.Estado == "Pendiente" && EsAdmin;
            dataGridDetalles.ReadOnly = !esEditable;
            btnGuardar.Visible = esEditable;
            btnEliminar.Visible = esEditable;


            ActualizarTotal();
        }


        private void ConfigurarColumnas()
        {
            dataGridDetalles.AutoGenerateColumns = false;
            dataGridDetalles.Columns.Clear();


            dataGridDetalles.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto", ReadOnly = true, Width = 250 });
            dataGridDetalles.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad" });

            dataGridDetalles.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioUnitario", HeaderText = "Precio Unitario", DefaultCellStyle = { Format = "C" }, ReadOnly = true });

            dataGridDetalles.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Subtotal", HeaderText = "Subtotal", DefaultCellStyle = { Format = "C" }, ReadOnly = true });


            foreach (DataGridViewColumn col in dataGridDetalles.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
        private void dataGridDetalles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridDetalles.Columns[e.ColumnIndex].DataPropertyName == "Cantidad")
            {
                dataGridDetalles.InvalidateRow(e.RowIndex);
                ActualizarTotal();
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
