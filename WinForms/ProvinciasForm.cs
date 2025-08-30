using DTOs;
using ApiClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class ProvinciasForm : Form
    {
        public ProvinciasForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(this.Form_Load);
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            btnActualizarProvincia.Enabled = false;
            btnEliminarProvincia.Enabled = false;
            ConfigurarColumnasProvincias();
            await CargarProvinciasAsync();
        }
        private void ConfigurarColumnasProvincias()
        {
            dgvProvincias.AutoGenerateColumns = false;
            dgvProvincias.Columns.Clear();
            dgvProvincias.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CodigoProvincia",
                HeaderText = "Código",
                Name = "colCodigo",
                Width = 120,
                ReadOnly = true
            });
            dgvProvincias.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreProvincia",
                HeaderText = "Nombre Provincia",
                Name = "colNombre",
                Width = 190,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }
        private async Task CargarProvinciasAsync()
        {
            try
            {
                var provincias = await ProvinciaApiClient.GetProvinciasAsync();
                dgvProvincias.DataSource = provincias;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar provincias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnAgregarProvincia_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCodigoProvincia.Text, out int codigo) && !string.IsNullOrWhiteSpace(txtNombreProvincia.Text))
            {
                var nuevaProvincia = new ProvinciaDto { CodigoProvincia = codigo, NombreProvincia = txtNombreProvincia.Text };
                try
                {
                    await ProvinciaApiClient.AddProvinciaAsync(nuevaProvincia);
                    MessageBox.Show("Provincia agregada con éxito.");
                    await CargarProvinciasAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar la provincia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un código y nombre válidos.", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void btnActualizarProvincia_Click(object sender, EventArgs e)
        {
            if (dgvProvincias.SelectedRows.Count > 0)
            {
                if (int.TryParse(txtCodigoProvincia.Text, out int codigo) && !string.IsNullOrWhiteSpace(txtNombreProvincia.Text))
                {
                    var provinciaActualizada = new ProvinciaDto { CodigoProvincia = codigo, NombreProvincia = txtNombreProvincia.Text };
                    try
                    {
                        await ProvinciaApiClient.UpdateProvinciaAsync(provinciaActualizada);
                        MessageBox.Show("Provincia actualizada con éxito.");
                        await CargarProvinciasAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar la provincia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un código y nombre válidos.", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void btnEliminarProvincia_Click(object sender, EventArgs e)
        {
            if (dgvProvincias.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dgvProvincias.SelectedRows[0];
                var provincia = filaSeleccionada.DataBoundItem as ProvinciaDto;
                if (MessageBox.Show($"¿Está seguro de que desea eliminar la provincia con código {provincia.CodigoProvincia}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        await ProvinciaApiClient.DeleteProvinciaAsync(provincia.CodigoProvincia);
                        MessageBox.Show("Provincia eliminada con éxito.");
                        await CargarProvinciasAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar la provincia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dgvProvincias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProvincias.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dgvProvincias.SelectedRows[0];
                var provincia = filaSeleccionada.DataBoundItem as ProvinciaDto;
                txtCodigoProvincia.Text = provincia.CodigoProvincia.ToString();
                txtNombreProvincia.Text = provincia.NombreProvincia;
                btnActualizarProvincia.Enabled = true;
                btnEliminarProvincia.Enabled = true;
                btnAgregarProvincia.Enabled = false;
                txtCodigoProvincia.ReadOnly = true;
            }
            else
            {
                txtCodigoProvincia.Text = string.Empty;
                txtNombreProvincia.Text = string.Empty;
                btnActualizarProvincia.Enabled = false;
                btnEliminarProvincia.Enabled = false;
                btnAgregarProvincia.Enabled = true;
                txtCodigoProvincia.ReadOnly = false;
            }
        }
    }
}