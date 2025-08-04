using DTOs;
using WinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            btnActualizarProvincia.Enabled = false;
            btnEliminarProvincia.Enabled = false;
            btnActualizarTipoProducto.Enabled = false;
            btnEliminarTipoProducto.Enabled = false;

            ConfigurarColumnasProvincias();
            ConfigurarColumnasTiposProducto();

            await CargarProvinciasAsync();
            await CargarTiposProductoAsync();
        }
        private void ConfigurarColumnasProvincias()
        {
            dgvProvincias.AutoGenerateColumns = false;
            dgvProvincias.Columns.Clear();

            // Columna Código
            dgvProvincias.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CodigoProvincia",
                HeaderText = "Código",
                Name = "colCodigo",
                Width = 120,
                ReadOnly = true
            });

            // Columna Nombre
            dgvProvincias.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreProvincia",
                HeaderText = "Nombre Provincia",
                Name = "colNombre",
                Width = 190,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void ConfigurarColumnasTiposProducto()
        {
            dgvTiposProducto.AutoGenerateColumns = false;
            dgvTiposProducto.Columns.Clear();

            // Columna ID
            dgvTiposProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IdTipoProducto",
                HeaderText = "ID",
                Name = "colId",
                Width = 120,
                ReadOnly = true
            });

            // Columna Nombre
            dgvTiposProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreTipoProducto",
                HeaderText = "Tipo de Producto",
                Name = "colNombre",
                Width = 190,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }
        private async Task CargarProvinciasAsync()
        {
            try
            {
                var provincias = await ApiClient.GetProvinciasAsync();
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
                var nuevaProvincia = new ProvinciaDto { CodigoProvincia = codigo, NombreProvincia = txtNombreProvincia.Text };
                try
                {
                    await ApiClient.AddProvinciaAsync(nuevaProvincia);
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
                        await ApiClient.UpdateProvinciaAsync(provinciaActualizada);
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
                        await ApiClient.DeleteProvinciaAsync(provincia.CodigoProvincia);
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

        private async Task CargarTiposProductoAsync()
        {
            try
            {
                var tiposProducto = await ApiClient.GetTiposProductoAsync();
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
                var nuevoTipoProducto = new TipoProductoDto { IdTipoProducto = id, NombreTipoProducto = txtNombreTipoProducto.Text };
                try
                {
                    await ApiClient.AddTipoProductoAsync(nuevoTipoProducto);
                    MessageBox.Show("Tipo de producto agregado con éxito.");
                    await CargarTiposProductoAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar el tipo de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    var tipoProductoActualizado = new TipoProductoDto { IdTipoProducto = id, NombreTipoProducto = txtNombreTipoProducto.Text };
                    try
                    {
                        await ApiClient.UpdateTipoProductoAsync(tipoProductoActualizado);
                        MessageBox.Show("Tipo de producto actualizado con éxito.");
                        await CargarTiposProductoAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar el tipo de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var tipoProducto = filaSeleccionada.DataBoundItem as TipoProductoDto;

                if (MessageBox.Show($"¿Está seguro de que desea eliminar el tipo de producto con ID {tipoProducto.IdTipoProducto}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        await ApiClient.DeleteTipoProductoAsync(tipoProducto.IdTipoProducto);
                        MessageBox.Show("Tipo de producto eliminado con éxito.");
                        await CargarTiposProductoAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el tipo de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var tipoProducto = filaSeleccionada.DataBoundItem as TipoProductoDto;
                txtIdTipoProducto.Text = tipoProducto.IdTipoProducto.ToString();
                txtNombreTipoProducto.Text = tipoProducto.NombreTipoProducto;
                btnActualizarTipoProducto.Enabled = true;
                btnEliminarTipoProducto.Enabled = true;
                btnAgregarTipoProducto.Enabled = false;
                txtIdTipoProducto.ReadOnly = true;
            }
            else
            {
                txtIdTipoProducto.Text = string.Empty;
                txtNombreTipoProducto.Text = string.Empty;
                btnActualizarTipoProducto.Enabled = false;
                btnEliminarTipoProducto.Enabled = false;
                btnAgregarTipoProducto.Enabled = true;
                txtIdTipoProducto.ReadOnly = false;
            }
        }

        private void dgvProvincias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void dgvTiposProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
