using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApiClient;
using DominioModelo;

namespace WinForms
{
    public partial class TiposProductoForm : Form
    {
        private readonly TipoProductoApiClient _apiClient;
        private DataGridView dgvTiposProducto;
        private TextBox txtDescripcion;
        private Button btnAgregar, btnActualizar, btnEliminar;

        public TiposProductoForm(TipoProductoApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            this.Text = "Gestión de Tipos de Producto";
            this.Load += TiposProductoForm_Load;
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Configuración del DataGridView
            dgvTiposProducto = new DataGridView
            {
                Location = new System.Drawing.Point(12, 12),
                Size = new System.Drawing.Size(400, 200),
                ColumnHeadersVisible = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvTiposProducto);

            // Campos para entrada de datos
            var lblDescripcion = new Label { Text = "Descripción:", Location = new System.Drawing.Point(12, 220) };
            txtDescripcion = new TextBox { Location = new System.Drawing.Point(80, 220), Size = new System.Drawing.Size(200, 20) };
            this.Controls.Add(lblDescripcion);
            this.Controls.Add(txtDescripcion);

            // Botones de acción
            btnAgregar = new Button { Text = "Agregar", Location = new System.Drawing.Point(12, 250), Size = new System.Drawing.Size(75, 23) };
            btnActualizar = new Button { Text = "Actualizar", Location = new System.Drawing.Point(93, 250), Size = new System.Drawing.Size(75, 23) };
            btnEliminar = new Button { Text = "Eliminar", Location = new System.Drawing.Point(174, 250), Size = new System.Drawing.Size(75, 23) };

            this.Controls.Add(btnAgregar);
            this.Controls.Add(btnActualizar);
            this.Controls.Add(btnEliminar);

            // Asignación de eventos
            btnAgregar.Click += BtnAgregar_Click;
            btnActualizar.Click += BtnActualizar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            dgvTiposProducto.SelectionChanged += DgvTiposProducto_SelectionChanged;
        }

        private async void TiposProductoForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            try
            {
                var tiposProducto = await _apiClient.GetAllAsync();
                dgvTiposProducto.DataSource = tiposProducto;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}");
            }
        }

        private async void BtnAgregar_Click(object sender, EventArgs e)
        {
            var nuevoTipo = new TipoProducto { Descripcion = txtDescripcion.Text };
            try
            {
                await _apiClient.CreateAsync(nuevoTipo);
                await LoadDataAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar tipo de producto: {ex.Message}");
            }
        }

        private async void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvTiposProducto.SelectedRows.Count > 0)
            {
                var tipoSeleccionado = dgvTiposProducto.SelectedRows[0].DataBoundItem as TipoProducto;
                if (tipoSeleccionado != null)
                {
                    var tipoActualizado = new TipoProducto
                    {
                        IdTipoProducto = tipoSeleccionado.IdTipoProducto,
                        Descripcion = txtDescripcion.Text
                    };
                    try
                    {
                        await _apiClient.UpdateAsync(tipoActualizado.IdTipoProducto, tipoActualizado);
                        await LoadDataAsync();
                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar tipo de producto: {ex.Message}");
                    }
                }
            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTiposProducto.SelectedRows.Count > 0)
            {
                var tipoSeleccionado = dgvTiposProducto.SelectedRows[0].DataBoundItem as TipoProducto;
                if (tipoSeleccionado != null)
                {
                    var confirmResult = MessageBox.Show(
                        $"¿Estás seguro de que quieres eliminar el tipo de producto '{tipoSeleccionado.Descripcion}'?",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        try
                        {
                            await _apiClient.DeleteAsync(tipoSeleccionado.IdTipoProducto);
                            await LoadDataAsync();
                            ClearInputs();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al eliminar tipo de producto: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void DgvTiposProducto_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTiposProducto.SelectedRows.Count > 0)
            {
                var tipoSeleccionado = dgvTiposProducto.SelectedRows[0].DataBoundItem as TipoProducto;
                if (tipoSeleccionado != null)
                {
                    txtDescripcion.Text = tipoSeleccionado.Descripcion;
                }
            }
        }

        private void ClearInputs()
        {
            txtDescripcion.Clear();
        }
    }
}
