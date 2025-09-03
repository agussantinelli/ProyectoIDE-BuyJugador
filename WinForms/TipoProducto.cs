using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class TipoProducto : Form
    {
        private readonly TipoProductoApiClient _tipoProductoApiClient;

        public TipoProducto()
        {
            InitializeComponent();
        }

        public TipoProducto(TipoProductoApiClient tipoProductoApiClient) : this()
        {
            _tipoProductoApiClient = tipoProductoApiClient;
        }

        private async void TipoProducto_Load(object sender, EventArgs e)
        {
            if (_tipoProductoApiClient == null) return;
            try
            {
                List<TipoProductoDTO>? tiposProducto = await _tipoProductoApiClient.GetAllAsync();
                // Verificamos que la lista no sea nula antes de asignarla
                if (tiposProducto != null)
                {
                    this.dgvTiposProducto.DataSource = tiposProducto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los tipos de producto: {ex.Message}");
            }
        }
    }
}
