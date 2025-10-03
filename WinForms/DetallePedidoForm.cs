using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class DetallePedidoForm : BaseForm
    {
        public PedidoDTO Pedido { get; set; }
        public bool EsAdmin { get; set; } // Para habilitar/deshabilitar controles

        private readonly LineaPedidoApiClient _lineaPedidoApiClient;
        private List<LineaPedidoDTO> _lineas;

        public DetallePedidoForm(LineaPedidoApiClient lineaPedidoApiClient)
        {
            InitializeComponent();
            _lineaPedidoApiClient = lineaPedidoApiClient;

            StyleManager.ApplyDataGridViewStyle(dataGridDetalle);
            StyleManager.ApplySecondaryButtonStyle(btnCerrar);
        }

        private async void DetallePedidoForm_Load(object sender, EventArgs e)
        {
            if (Pedido == null)
            {
                MessageBox.Show("No se ha proporcionado un pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Cargar datos del encabezado
            lblIdPedido.Text = $"Pedido Nro: {Pedido.IdPedido}";
            lblFecha.Text = $"Fecha: {Pedido.Fecha:dd/MM/yyyy}";
            lblProveedor.Text = $"Proveedor: {Pedido.ProveedorRazonSocial}";
            lblTotal.Text = $"Total: {Pedido.Total:C}";
            lblEstado.Text = $"Estado: {Pedido.Estado}";

            // Cargar líneas de pedido
            try
            {
                _lineas = await _lineaPedidoApiClient.GetLineasByPedidoIdAsync(Pedido.IdPedido);
                dataGridDetalle.DataSource = _lineas;
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el detalle del pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrid()
        {
            dataGridDetalle.AutoGenerateColumns = false;
            dataGridDetalle.Columns.Clear();

            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad" });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioUnitario", HeaderText = "Precio Unit.", DefaultCellStyle = new DataGridViewCellStyle { Format = "C" } });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Subtotal", HeaderText = "Subtotal", DefaultCellStyle = new DataGridViewCellStyle { Format = "C" } });
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
