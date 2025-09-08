using ApiClient;
using System;
using System.Windows.Forms;

namespace WinForms
{
    // La palabra "partial" es fundamental para que el diseñador funcione.
    public partial class MainForm : Form
    {
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;

        // Este constructor recibe las herramientas para hablar con la API.
        public MainForm(ProvinciaApiClient provinciaApiClient, TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent(); // Esto ya no dará error.
            _provinciaApiClient = provinciaApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;
        }

        private void btnProvincia_Click(object sender, EventArgs e)
        {
            // Pasamos el cliente de la API al nuevo formulario.
            var provinciaForm = new Provincia(_provinciaApiClient);
            provinciaForm.ShowDialog();
        }

        private void btnTipoProducto_Click(object sender, EventArgs e)
        {
            var tipoProductoForm = new TipoProducto(_tipoProductoApiClient);
            tipoProductoForm.ShowDialog();
        }

        private void btnMenuProductos_Click(object sender, EventArgs e)
        {
            ProductoForm formProductos = new ProductoForm();
            formProductos.Show();
        }
    }
}

