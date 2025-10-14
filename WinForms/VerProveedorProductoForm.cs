using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class VerProveedoresProductoForm : BaseForm
    {
        private readonly ProveedorApiClient _proveedorApiClient;
        private int _idProducto;
        private string _nombreProducto;

        public VerProveedoresProductoForm(ProveedorApiClient proveedorApiClient)
        {
            InitializeComponent();
            _proveedorApiClient = proveedorApiClient;
            StyleManager.ApplyDataGridViewStyle(dgvProveedores);
            StyleManager.ApplyButtonStyle(btnCerrar);
        }

        public void CargarDatos(int idProducto, string nombreProducto)
        {
            _idProducto = idProducto;
            _nombreProducto = nombreProducto;
            this.Text = $"Proveedores de {_nombreProducto}";
        }

        private async void VerProveedoresProductoForm_Load(object sender, EventArgs e)
        {
            try
            {
                var proveedores = await _proveedorApiClient.GetProveedoresByProductoIdAsync(_idProducto);
                ConfigurarColumnas();
                dgvProveedores.DataSource = proveedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los proveedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvProveedores.AutoGenerateColumns = false;
            dgvProveedores.Columns.Clear();
            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdProveedor", HeaderText = "ID", Width = 60 });
            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RazonSocial", HeaderText = "Razón Social", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cuit", HeaderText = "CUIT", Width = 150 });
            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 200 });
            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Telefono", HeaderText = "Teléfono", Width = 150 });
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
