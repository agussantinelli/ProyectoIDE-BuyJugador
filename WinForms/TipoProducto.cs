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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTiposProducto_SelectionChanged(object sender, EventArgs e)
        {
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var descripcion = txtDescripcion.Text.Trim();

                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("La descripción no puede estar vacía.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new TipoProductoDTO
                {
                    Descripcion = descripcion
                };

                if (_selectedTipoProductoId == 0)
                {
                    await _tipoProductoApiClient.CreateAsync(dto);
                    MessageBox.Show("Tipo de producto creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dto.IdTipoProducto = _selectedTipoProductoId;
                    await _tipoProductoApiClient.UpdateAsync(_selectedTipoProductoId, dto);
                    MessageBox.Show("Tipo de producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await CargarTiposProducto();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar tipo de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedTipoProductoId == 0)
                {
                    MessageBox.Show("Seleccione un tipo de producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Tipo de producto eliminado exitosamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await CargarTiposProducto();
                    LimpiarFormulario();
                }
            }
            catch (HttpRequestException exH)
            {
                if (exH.Message.Contains("409"))
                {
                    MessageBox.Show("No se puede eliminar este tipo de producto porque tiene productos asociados.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"Error al eliminar tipo de producto: {exH.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar tipo de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtDescripcion.Clear();
            _selectedTipoProductoId = 0;
            dgvTiposProducto.ClearSelection();
        }
    }
}
