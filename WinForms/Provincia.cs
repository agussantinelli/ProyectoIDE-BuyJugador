using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Provincia : Form
    {
        private readonly ProvinciaApiClient _provinciaApiClient;

        public Provincia(ProvinciaApiClient provinciaApiClient)
        {
            InitializeComponent();
            _provinciaApiClient = provinciaApiClient;
        }

        private async void Provincia_Load(object sender, EventArgs e)
        {
            try
            {
                List<ProvinciaDTO>? provincias = await _provinciaApiClient.GetAllAsync();
                if (provincias != null)
                {
                    dgvProvincias.DataSource = provincias;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar provincias: {ex.Message}");
            }
        }
    }
}
