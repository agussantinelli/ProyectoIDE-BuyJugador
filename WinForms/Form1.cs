using DTOs;
using WinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private readonly ApiService _apiService;

        public Form1()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await CargarProvinciasAsync();
            await CargarTiposProductoAsync();
        }

        // --- Lógica para Provincias ---

        private async Task CargarProvinciasAsync()
        {
            try
            {
                var provincias = await _apiService.GetProvinciasAsync();
                dgvProvincias.DataSource = provincias;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar provincias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCargarProvincias_Click(object sender, EventArgs e)
        {
            await CargarProvinciasAsync();
        }

        private async void btnAgregarProvincia_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCodigoProvincia.Text, out int codigo) && !string.IsNullOrWhiteSpace(txtNombreProvincia.Text))
            {
                var nuevaProvincia = new Provincia { CodigoProvincia = codigo, NombreProvincia = txtNombreProvincia.Text };
                bool exito = await _apiService.AddProvinciaAsync(nuevaProvincia);

                if (exito)
                {
                    MessageBox.Show("Provincia agregada con éxito.");
                    await CargarProvinciasAsync();
                }
                else
                {
                    MessageBox.Show("Error al agregar la provincia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    var provinciaActualizada = new Provincia { CodigoProvincia = codigo, NombreProvincia = txtNombreProvincia.Text };
                    bool exito = await _apiService.UpdateProvinciaAsync(provinciaActualizada);

                    if (exito)
                    {
                        MessageBox.Show("Provincia actualizada con éxito.");
                        await CargarProvinciasAsync();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar la provincia. Puede que no exista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var codigoProvincia = (int)filaSeleccionada.Cells["CodigoProvincia"].Value;

                if (MessageBox.Show($"¿Está seguro de que desea eliminar la provincia con código {codigoProvincia}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool exito = await _apiService.DeleteProvinciaAsync(codigoProvincia);
                    if (exito)
                    {
                        MessageBox.Show("Provincia eliminada con éxito.");
                        await CargarProvinciasAsync();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar la provincia. Puede que no exista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txtCodigoProvincia.Text = filaSeleccionada.Cells["CodigoProvincia"].Value.ToString();
                txtNombreProvincia.Text = filaSeleccionada.Cells["NombreProvincia"].Value.ToString();
            }
        }

        // --- Lógica para Tipos de Producto ---

        private async Task CargarTiposProductoAsync()
        {
            try
            {
                var tiposProducto = await _apiService.GetTiposProductoAsync();
                dgvTiposProducto.DataSource = tiposProducto;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCargarTiposProducto_Click(object sender, EventArgs e)
        {
            await CargarTiposProductoAsync();
        }

        private async void btnAgregarTipoProducto_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIdTipoProducto.Text, out int id) && !string.IsNullOrWhiteSpace(txtNombreTipoProducto.Text))
            {
                var nuevoTipoProducto = new TipoProducto { IdTipoProducto = id, NombreTipoProducto = txtNombreTipoProducto.Text };
                bool exito = await _apiService.AddTipoProductoAsync(nuevoTipoProducto);

                if (exito)
                {
                    MessageBox.Show("Tipo de producto agregado con éxito.");
                    await CargarTiposProductoAsync();
                }
                else
                {
                    MessageBox.Show("Error al agregar el tipo de producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un ID y nombre válidos.", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnActualizarTipoProducto_Click(object sender, EventArgs e)
        {
            if (dgvTiposProducto.SelectedRows.Count > 0)
            {
                if (int.TryParse(txtIdTipoProducto.Text, out int id) && !string.IsNullOrWhiteSpace(txtNombreTipoProducto.Text))
                {
                    var tipoProductoActualizado = new TipoProducto { IdTipoProducto = id, NombreTipoProducto = txtNombreTipoProducto.Text };
                    bool exito = await _apiService.UpdateTipoProductoAsync(tipoProductoActualizado);

                    if (exito)
                    {
                        MessageBox.Show("Tipo de producto actualizado con éxito.");
                        await CargarTiposProductoAsync();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el tipo de producto. Puede que no exista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un ID y nombre válidos.", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnEliminarTipoProducto_Click(object sender, EventArgs e)
        {
            if (dgvTiposProducto.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dgvTiposProducto.SelectedRows[0];
                var id = (int)filaSeleccionada.Cells["IdTipoProducto"].Value;

                if (MessageBox.Show($"¿Está seguro de que desea eliminar el tipo de producto con ID {id}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool exito = await _apiService.DeleteTipoProductoAsync(id);
                    if (exito)
                    {
                        MessageBox.Show("Tipo de producto eliminado con éxito.");
                        await CargarTiposProductoAsync();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el tipo de producto. Puede que no exista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvTiposProducto_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTiposProducto.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dgvTiposProducto.SelectedRows[0];
                txtIdTipoProducto.Text = filaSeleccionada.Cells["IdTipoProducto"].Value.ToString();
                txtNombreTipoProducto.Text = filaSeleccionada.Cells["NombreTipoProducto"].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProvincias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}