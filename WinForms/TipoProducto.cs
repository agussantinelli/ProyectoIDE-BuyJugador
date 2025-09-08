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

        public TipoProducto(TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _tipoProductoApiClient = tipoProductoApiClient;
        }

        private async void TipoProducto_Load(object sender, EventArgs e)
        {
            try
            {
                List<TipoProductoDTO>? tiposProducto = await _tipoProductoApiClient.GetAllAsync();
                if (tiposProducto != null)
                {
                    dgvTiposProducto.DataSource = tiposProducto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}");
            }
        }
    }
}
