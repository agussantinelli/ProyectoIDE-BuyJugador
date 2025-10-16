using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class VerProductosTipoForm : BaseForm
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly int _idTipoProducto;
        private readonly string _descripcionTipoProducto;

        public VerProductosTipoForm(ProductoApiClient productoApiClient, int idTipoProducto, string descripcionTipoProducto)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _idTipoProducto = idTipoProducto;
            _descripcionTipoProducto = descripcionTipoProducto;

            // Apply styles
            StyleManager.ApplyDataGridViewStyle(dgvProductos);
            StyleManager.ApplyButtonStyle(btnVolver);
        }

        private async void VerProductosTipoForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Productos del Tipo: '{_descripcionTipoProducto}'";
            lblTitulo.Text = this.Text;
            await CargarProductos();
        }

        private async Task CargarProductos()
        {
            try
            {
                // Set cursor to wait
                this.Cursor = Cursors.WaitCursor;
                var productos = await _productoApiClient.GetByTipoProductoIdAsync(_idTipoProducto) ?? new List<ProductoDTO>();
                dgvProductos.DataSource = productos.ToList();
                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restore cursor
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarColumnas()
        {
            // Configure visible columns
            if (dgvProductos.Columns.Contains("IdProducto"))
            {
                dgvProductos.Columns["IdProducto"].HeaderText = "Código";
                dgvProductos.Columns["IdProducto"].Width = 80;
            }
            if (dgvProductos.Columns.Contains("Nombre"))
            {
                dgvProductos.Columns["Nombre"].HeaderText = "Nombre del Producto";
                dgvProductos.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvProductos.Columns.Contains("Stock"))
            {
                dgvProductos.Columns["Stock"].HeaderText = "Stock";
                dgvProductos.Columns["Stock"].Width = 100;
            }
            if (dgvProductos.Columns.Contains("PrecioActual"))
            {
                dgvProductos.Columns["PrecioActual"].HeaderText = "Precio Actual";
                dgvProductos.Columns["PrecioActual"].DefaultCellStyle.Format = "C2";
                dgvProductos.Columns["PrecioActual"].Width = 120;
            }

            // Hide unnecessary columns
            var columnasAOcultar = new string[] { "Descripcion", "IdTipoProducto", "TipoProductoDescripcion", "PrecioCompra", "Precios" };
            foreach (string colName in columnasAOcultar)
            {
                if (dgvProductos.Columns.Contains(colName))
                {
                    dgvProductos.Columns[colName].Visible = false;
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => this.Close();
    }
}
