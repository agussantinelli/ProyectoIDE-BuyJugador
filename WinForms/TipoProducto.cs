using ApiClient;
using DTOs;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            await CargarTiposProducto();
        }

        private async Task CargarTiposProducto()
        {
            try
            {
                List<TipoProductoDTO>? tiposProducto = await _tipoProductoApiClient.GetAllAsync();
                if (tiposProducto != null)
                {
                    // Evita que el usuario agregue filas directamente en el DataGridView
                    dgvTiposProducto.AllowUserToAddRows = false;
                    dgvTiposProducto.DataSource = tiposProducto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}");
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // Solicita al usuario el nombre para el nuevo tipo de producto
            string descripcion = Interaction.InputBox("Ingrese la descripción del nuevo tipo de producto:", "Agregar Tipo de Producto", "");
            if (!string.IsNullOrWhiteSpace(descripcion))
            {
                try
                {
                    var nuevoTipo = new TipoProductoDTO { Descripcion = descripcion };
                    var creado = await _tipoProductoApiClient.CreateAsync(nuevoTipo);
                    if (creado != null)
                    {
                        MessageBox.Show("Tipo de producto agregado exitosamente.");
                        await CargarTiposProducto(); // Recarga la lista
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar el tipo de producto: {ex.Message}");
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada
            if (dgvTiposProducto.CurrentRow != null)
            {
                var tipoSeleccionado = (TipoProductoDTO)dgvTiposProducto.CurrentRow.DataBoundItem;

                // Pide la nueva descripción, mostrando la actual por defecto
                string nuevaDescripcion = Interaction.InputBox("Ingrese la nueva descripción:", "Editar Tipo de Producto", tipoSeleccionado.Descripcion);

                if (!string.IsNullOrWhiteSpace(nuevaDescripcion) && nuevaDescripcion != tipoSeleccionado.Descripcion)
                {
                    try
                    {
                        tipoSeleccionado.Descripcion = nuevaDescripcion;
                        await _tipoProductoApiClient.UpdateAsync(tipoSeleccionado.IdTipoProducto, tipoSeleccionado);
                        {
                            MessageBox.Show("Tipo de producto actualizado exitosamente.");
                            await CargarTiposProducto(); // Recarga la lista
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al editar el tipo de producto: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un tipo de producto para editar.");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTiposProducto.CurrentRow != null)
            {
                var tipoSeleccionado = (TipoProductoDTO)dgvTiposProducto.CurrentRow.DataBoundItem;
                var confirmacion = MessageBox.Show($"¿Está seguro de que desea eliminar '{tipoSeleccionado.Descripcion}'?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        await _tipoProductoApiClient.DeleteAsync(tipoSeleccionado.IdTipoProducto);
                        MessageBox.Show("Tipo de producto eliminado exitosamente.");
                        await CargarTiposProducto(); // Recarga la lista
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el tipo de producto: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un tipo de producto para eliminar.");
            }
        }
    }
}
