using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApiClient;
using DominioModelo;

namespace WinForms
{
    public partial class ProvinciasForm : Form
    {
        private readonly ProvinciaApiClient _apiClient;
        private DataGridView dgvProvincias;
        private TextBox txtNombre;
        private Button btnAgregar, btnActualizar, btnEliminar;

        public ProvinciasForm(ProvinciaApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            this.Text = "Gestión de Provincias";
            this.Load += ProvinciasForm_Load;
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Configuración del DataGridView
            dgvProvincias = new DataGridView
            {
                Location = new System.Drawing.Point(12, 12),
                Size = new System.Drawing.Size(400, 200),
                ColumnHeadersVisible = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvProvincias);

            // Campos para entrada de datos
            var lblNombre = new Label { Text = "Nombre:", Location = new System.Drawing.Point(12, 220) };
            txtNombre = new TextBox { Location = new System.Drawing.Point(80, 220), Size = new System.Drawing.Size(200, 20) };
            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);

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
            dgvProvincias.SelectionChanged += DgvProvincias_SelectionChanged;
        }

        private async void ProvinciasForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            try
            {
                var provincias = await _apiClient.GetAllAsync();
                dgvProvincias.DataSource = provincias;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar provincias: {ex.Message}");
            }
        }

        private async void BtnAgregar_Click(object sender, EventArgs e)
        {
            var nuevaProvincia = new Provincia { Nombre = txtNombre.Text };
            try
            {
                await _apiClient.CreateAsync(nuevaProvincia);
                await LoadDataAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar provincia: {ex.Message}");
            }
        }

        private async void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvProvincias.SelectedRows.Count > 0)
            {
                var provinciaSeleccionada = dgvProvincias.SelectedRows[0].DataBoundItem as Provincia;
                if (provinciaSeleccionada != null)
                {
                    var provinciaActualizada = new Provincia
                    {
                        Id = provinciaSeleccionada.Id,
                        Nombre = txtNombre.Text
                    };
                    try
                    {
                        await _apiClient.UpdateAsync(provinciaActualizada.Id, provinciaActualizada);
                        await LoadDataAsync();
                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar provincia: {ex.Message}");
                    }
                }
            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProvincias.SelectedRows.Count > 0)
            {
                var provinciaSeleccionada = dgvProvincias.SelectedRows[0].DataBoundItem as Provincia;
                if (provinciaSeleccionada != null)
                {
                    var confirmResult = MessageBox.Show($"¿Estás seguro de que quieres eliminar la provincia '{provinciaSeleccionada.Nombre}'?", "Confirmar Eliminación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        try
                        {
                            await _apiClient.DeleteAsync(provinciaSeleccionada.Id);
                            await LoadDataAsync();
                            ClearInputs();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al eliminar provincia: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void DgvProvincias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProvincias.SelectedRows.Count > 0)
            {
                var provinciaSeleccionada = dgvProvincias.SelectedRows[0].DataBoundItem as Provincia;
                if (provinciaSeleccionada != null)
                {
                    txtNombre.Text = provinciaSeleccionada.Nombre;
                }
            }
        }

        private void ClearInputs()
        {
            txtNombre.Clear();
        }

        private void ProvinciasForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
