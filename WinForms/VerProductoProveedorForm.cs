using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class VerProductosProveedorForm : BaseForm
    {
        private readonly ProductoApiClient _productoApiClient;
        private int _idProveedor;
        private string _razonSocial;

        public VerProductosProveedorForm(ProductoApiClient productoApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            StyleManager.ApplyDataGridViewStyle(dgvProductos);
            StyleManager.ApplyButtonStyle(btnCerrar);
        }

        public void CargarDatos(int idProveedor, string razonSocial)
        {
            _idProveedor = idProveedor;
            _razonSocial = razonSocial;
            this.Text = $"Productos de {_razonSocial}";
        }

        private async void VerProductosProveedorForm_Load(object sender, EventArgs e)
        {
            try
            {
                var productos = await _productoApiClient.GetProductosByProveedorIdAsync(_idProveedor);
                ConfigurarColumnas();
                dgvProductos.DataSource = productos?.Select(p => new
                {
                    p.IdProducto,
                    p.Nombre,
                    p.Descripcion,
                    p.PrecioCompra
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdProducto", HeaderText = "ID", Width = 60 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Nombre", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Descripcion", HeaderText = "Descripción", Width = 300 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioCompra", HeaderText = "Precio Compra", Width = 150, DefaultCellStyle = { Format = "C2" } });
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
