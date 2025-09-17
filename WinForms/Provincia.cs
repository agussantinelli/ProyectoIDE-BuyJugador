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

                    if (dgvProvincias.Columns.Contains("IdProvincia"))
                    {
                        dgvProvincias.Columns["IdProvincia"].HeaderText = "Código";
                        dgvProvincias.Columns["IdProvincia"].Width = 100;
                    }

                    if (dgvProvincias.Columns.Contains("Nombre"))
                    {
                        dgvProvincias.Columns["Nombre"].HeaderText = "Provincia";
                        dgvProvincias.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar provincias: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();
    }
}
