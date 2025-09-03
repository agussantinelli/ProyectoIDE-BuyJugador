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

        public Provincia()
        {
            InitializeComponent();
        }

        public Provincia(ProvinciaApiClient provinciaApiClient) : this()
        {
            _provinciaApiClient = provinciaApiClient;
        }

        private async void Provincia_Load(object sender, EventArgs e)
        {
            if (_provinciaApiClient == null) return;
            try
            {
                List<ProvinciaDTO>? provincias = await _provinciaApiClient.GetAllAsync();
                // Verificamos que la lista no sea nula antes de asignarla
                if (provincias != null)
                {
                    this.dgvProvincias.DataSource = provincias;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar las provincias: {ex.Message}");
            }
        }
    }
}
