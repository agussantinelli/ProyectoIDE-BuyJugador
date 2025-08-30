using DTOs;
using ApiClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class TiposProductoForm : Form
    {
        public TiposProductoForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(this.Form_Load);
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            btnActualizarTipoProducto.Enabled = false;
            btnEliminarTipoProducto.Enabled = false;
            ConfigurarColumnasTiposProducto();
            await CargarTiposProductoAsync();
        }
        private void ConfigurarColumnasTiposProducto()
        {
            dgvTiposProducto.AutoGenerateColumns = false;
            dgvTiposProducto.Columns.Clear();
            dgvTiposProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IdTipoProducto",
                HeaderText = "ID",
                Name = "colId",
                Width = 120,
                ReadOnly = true
            });
            dgvTiposProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreTipoProducto",
                HeaderText = "Tipo de Producto",
                Name = "colNombre",
                Width = 190,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }
        private async Task CargarTiposProductoAsync()
        {
            try
            {
                var tiposProducto = await TipoProductoApiClient.GetTiposProductoAsync();
                dgvTiposProducto.DataSource = tiposProducto;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnAgregarTipoProducto_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIdTipoProducto.Text, out int id) && !string.IsNullOrWhiteSpace(txtNombreTipoProducto.Text))
            {
                var nuevoTipoProducto = new TipoProductoDto { IdTipoProducto = id, NombreTipoProducto = txtNombreTipoProducto.Text };
                try
                {
                    await TipoProductoApiClient.AddTipoProductoAsync(nuevoTipoProducto);
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
                        await TipoProductoApiClient.UpdateTipoProductoAsync(tipoProductoActualizado);
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
                        await TipoProductoApiClient.DeleteTipoProductoAsync(tipoProducto.IdTipoProducto);
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
    }
}