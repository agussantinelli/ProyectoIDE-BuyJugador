using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class TipoProducto : Form
    {
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private int _selectedTipoProductoId = 0;
        private bool _isClearingSelection = false;

        public TipoProducto(TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _tipoProductoApiClient = tipoProductoApiClient;
        }

        private async void TipoProducto_Load(object sender, EventArgs e)
        {
            await CargarTiposProducto();
            LimpiarFormulario();
        }

        private async Task CargarTiposProducto()
        {
            try
            {
                List<TipoProductoDTO>? tiposProducto = await _tipoProductoApiClient.GetAllAsync();
                if (tiposProducto != null)
                {
                    dgvTiposProducto.DataSource = tiposProducto;

                    if (dgvTiposProducto.Columns.Contains("IdTipoProducto"))
                    {
                        dgvTiposProducto.Columns["IdTipoProducto"].HeaderText = "Código";
                        dgvTiposProducto.Columns["IdTipoProducto"].Width = 100;
                    }

                    if (dgvTiposProducto.Columns.Contains("Descripcion"))
                    {
                        dgvTiposProducto.Columns["Descripcion"].HeaderText = "Descripción";
                        dgvTiposProducto.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTiposProducto_SelectionChanged(object sender, EventArgs e)
        {
            if (_isClearingSelection) return;

            if (dgvTiposProducto.SelectedRows.Count > 0)
            {
                var selectedRow = dgvTiposProducto.SelectedRows[0];
                var tipoProducto = selectedRow.DataBoundItem as TipoProductoDTO;

                if (tipoProducto != null)
                {
                    txtDescripcion.Text = tipoProducto.Descripcion;
                    _selectedTipoProductoId = tipoProducto.IdTipoProducto;
                }
            }
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                var descripcion = txtDescripcion.Text.Trim();

                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("Debe ingresar una descripción para crear un nuevo tipo de producto.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new TipoProductoDTO { Descripcion = descripcion };

                var creado = await _tipoProductoApiClient.CreateAsync(dto);

                MessageBox.Show($"Tipo de producto '{creado.Descripcion}' creado exitosamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await CargarTiposProducto();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear tipo de producto: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedTipoProductoId == 0)
                {
                    MessageBox.Show("Seleccione un tipo de producto para actualizar.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var descripcion = txtDescripcion.Text.Trim();

                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("La descripción no puede estar vacía.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new TipoProductoDTO
                {
                    IdTipoProducto = _selectedTipoProductoId,
                    Descripcion = descripcion
                };

                await _tipoProductoApiClient.UpdateAsync(_selectedTipoProductoId, dto);

                MessageBox.Show("Tipo de producto actualizado exitosamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await CargarTiposProducto();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar tipo de producto: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedTipoProductoId == 0)
                {
                    MessageBox.Show("Seleccione un tipo de producto para eliminar.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show(
                    "¿Estás seguro que deseas eliminar este tipo de producto?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmResult == DialogResult.Yes)
                {
                    await _tipoProductoApiClient.DeleteAsync(_selectedTipoProductoId);
                    MessageBox.Show("Tipo de producto eliminado exitosamente.",
                                    "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await CargarTiposProducto();
                    LimpiarFormulario();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar tipo de producto: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            _isClearingSelection = true;

            txtDescripcion.Clear();
            _selectedTipoProductoId = 0;
            dgvTiposProducto.ClearSelection();

            _isClearingSelection = false;

            txtDescripcion.Focus();
        }
    }
}
