using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class PersonaForm : Form
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;

        private List<PersonaDTO> _activosCache = new();
        private List<PersonaDTO> _inactivosCache = new();
        private string _filtroActual = string.Empty;

        public PersonaForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _personaApiClient = serviceProvider.GetRequiredService<PersonaApiClient>();
            _provinciaApiClient = serviceProvider.GetRequiredService<ProvinciaApiClient>();
            _localidadApiClient = serviceProvider.GetRequiredService<LocalidadApiClient>();

            PrepararGrid(dgvActivos);
            PrepararGrid(dgvInactivos);

            this.StartPosition = FormStartPosition.CenterParent;

        }

        private void PrepararGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            // Columnas visibles
            AddTextCol(dgv, "NombreCompleto", "Nombre Completo", fill: true, minWidth: 150);
            AddTextCol(dgv, "Dni", "DNI", width: 100);
            AddTextCol(dgv, "Email", "Email", width: 190);
            AddTextCol(dgv, "Telefono", "Teléfono", width: 110);
            AddTextCol(dgv, "Direccion", "Dirección", width: 140);
            AddTextCol(dgv, "FechaIngresoFormateada", "Fecha Ingreso", width: 110);
            AddTextCol(dgv, "LocalidadNombre", "Localidad", width: 110);
            AddTextCol(dgv, "ProvinciaNombre", "Provincia", width: 100);
            AddTextCol(dgv, "Rol", "Rol", width: 80);

            // (No agregamos las columnas que querés ocultar: IdPersona, Password, IdLocalidad, FechaIngreso, Estado, EstadoDescripcion)
        }

        private static void AddTextCol(
            DataGridView dgv,
            string dataProperty,
            string header,
            int width = 100,
            bool fill = false,
            int minWidth = 50)
        {
            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                Name = dataProperty,
                HeaderText = header,
                ReadOnly = true
            };

            if (fill)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.MinimumWidth = minWidth;
            }
            else
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Width = width;
            }

            dgv.Columns.Add(col);
        }

        // === Ciclo de vida ===
        private async void PersonaForm_Load(object sender, EventArgs e)
        {
            await CargarTodo();
            ActualizarVisibilidadBotones();
        }

        private async System.Threading.Tasks.Task CargarTodo()
        {
            await CargarActivos();
            await CargarInactivos();
            AplicarFiltro();
        }

        private async System.Threading.Tasks.Task CargarActivos()
        {
            try
            {
                _activosCache = await _personaApiClient.GetAllAsync() ?? new List<PersonaDTO>();
                dgvActivos.DataSource = _activosCache.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personal activo: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task CargarInactivos()
        {
            try
            {
                _inactivosCache = await _personaApiClient.GetInactivosAsync() ?? new List<PersonaDTO>();
                dgvInactivos.DataSource = _inactivosCache.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ex-personal: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === Filtro ===
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var f = _filtroActual.ToLowerInvariant();

            bool Coincide(PersonaDTO p) =>
                (p.NombreCompleto?.ToLower().Contains(f) ?? false) ||
                p.Dni.ToString().Contains(f) ||
                (p.Email?.ToLower().Contains(f) ?? false) ||
                (p.Telefono?.ToLower().Contains(f) ?? false) ||
                (p.Direccion?.ToLower().Contains(f) ?? false) ||
                (p.LocalidadNombre?.ToLower().Contains(f) ?? false) ||
                (p.ProvinciaNombre?.ToLower().Contains(f) ?? false) ||
                (p.Rol?.ToLower().Contains(f) ?? false);

            if (tabControlPersonas.SelectedTab == tabPageActivos)
                dgvActivos.DataSource = string.IsNullOrWhiteSpace(f) ? _activosCache.ToList() : _activosCache.Where(Coincide).ToList();
            else
                dgvInactivos.DataSource = string.IsNullOrWhiteSpace(f) ? _inactivosCache.ToList() : _inactivosCache.Where(Coincide).ToList();
        }

        private PersonaDTO? ObtenerSeleccionado(DataGridView dgv)
            => dgv.SelectedRows.Count > 0 ? dgv.SelectedRows[0].DataBoundItem as PersonaDTO : null;

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = new CrearPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient);
            if (form.ShowDialog(this) == DialogResult.OK)
                await CargarActivos();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvActivos);
            if (persona == null) return;

            using var form = new EditarPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient, persona);
            if (form.ShowDialog(this) == DialogResult.OK)
                await CargarActivos();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvActivos);
            if (persona == null) return;

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea dar de baja a \"{persona.NombreCompleto}\"?",
                "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                await _personaApiClient.DeleteAsync(persona.IdPersona);
                await CargarTodo();
                MessageBox.Show("Persona dada de baja exitosamente.", "Baja Exitosa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dar de baja a la persona: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReactivar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvInactivos);
            if (persona == null) return;

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea reactivar a \"{persona.NombreCompleto}\"?",
                "Confirmar Reactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var response = await _personaApiClient.ReactivarAsync(persona.IdPersona);
                if (response.IsSuccessStatusCode)
                {
                    await CargarTodo();
                    MessageBox.Show("Persona reactivada exitosamente.", "Reactivación Exitosa",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo reactivar la persona.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reactivar a la persona: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void ActualizarVisibilidadBotones()
        {
            bool esTabActivos = tabControlPersonas.SelectedTab == tabPageActivos;

            bool activoSeleccionado = ObtenerSeleccionado(dgvActivos) != null;
            bool inactivoSeleccionado = ObtenerSeleccionado(dgvInactivos) != null;

            btnEditar.Visible = esTabActivos && activoSeleccionado;
            btnEliminar.Visible = esTabActivos && activoSeleccionado;
            btnReactivar.Visible = !esTabActivos && inactivoSeleccionado;
        }

        private void dgvActivos_SelectionChanged(object sender, EventArgs e) => ActualizarVisibilidadBotones();
        private void dgvInactivos_SelectionChanged(object sender, EventArgs e) => ActualizarVisibilidadBotones();

        private void tabControlPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            ActualizarVisibilidadBotones();
        }
    }
}