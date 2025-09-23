using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public PersonaForm(
            PersonaApiClient personaApiClient,
            ProvinciaApiClient provinciaApiClient,
            LocalidadApiClient localidadApiClient)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            _provinciaApiClient = provinciaApiClient;
            _localidadApiClient = localidadApiClient;
        }

        private async void PersonaForm_Load(object sender, EventArgs e)
        {
            await CargarTodo();
            ActualizarVisibilidadBotones();
        }

        private async Task CargarTodo()
        {
            await CargarActivos();
            await CargarInactivos();
            AplicarFiltro();
        }

        private async Task CargarActivos()
        {
            try
            {
                _activosCache = await _personaApiClient.GetAllAsync() ?? new List<PersonaDTO>();
                dgvActivos.DataSource = _activosCache.ToList();
                ConfigurarColumnas(dgvActivos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personal activo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarInactivos()
        {
            try
            {
                _inactivosCache = await _personaApiClient.GetInactivosAsync() ?? new List<PersonaDTO>();
                dgvInactivos.DataSource = _inactivosCache.ToList();
                ConfigurarColumnas(dgvInactivos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ex-personal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas(DataGridView dgv)
        {
            if (dgv.DataSource == null) return;
            dgv.Columns["IdPersona"].Visible = false;
            dgv.Columns["Password"].Visible = false;
            dgv.Columns["IdLocalidad"].Visible = false;
            dgv.Columns["FechaIngreso"].Visible = false;
            dgv.Columns["Estado"].Visible = false;
            dgv.Columns["EstadoDescripcion"].Visible = false;

            dgv.Columns["NombreCompleto"].HeaderText = "Nombre Completo";
            dgv.Columns["NombreCompleto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["NombreCompleto"].MinimumWidth = 150;

            dgv.Columns["Dni"].HeaderText = "DNI";
            dgv.Columns["Dni"].Width = 100;

            dgv.Columns["Email"].HeaderText = "Email";
            dgv.Columns["Email"].Width = 190;

            dgv.Columns["Telefono"].HeaderText = "Teléfono";
            dgv.Columns["Telefono"].Width = 110;

            dgv.Columns["Direccion"].HeaderText = "Dirección";
            dgv.Columns["Direccion"].Width = 140;

            dgv.Columns["FechaIngresoFormateada"].HeaderText = "Fecha Ingreso";
            dgv.Columns["FechaIngresoFormateada"].Width = 110;

            dgv.Columns["LocalidadNombre"].HeaderText = "Localidad";
            dgv.Columns["LocalidadNombre"].Width = 110;

            dgv.Columns["ProvinciaNombre"].HeaderText = "Provincia";
            dgv.Columns["ProvinciaNombre"].Width = 100;

            dgv.Columns["Rol"].HeaderText = "Rol";
            dgv.Columns["Rol"].Width = 80;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var f = _filtroActual.ToLowerInvariant();
            Func<PersonaDTO, bool> filtro = p =>
                (p.NombreCompleto?.ToLower().Contains(f) ?? false) ||
                p.Dni.ToString().Contains(f) ||
                (p.Email?.ToLower().Contains(f) ?? false) ||
                (p.Telefono?.ToLower().Contains(f) ?? false) ||
                (p.Direccion?.ToLower().Contains(f) ?? false) ||
                (p.LocalidadNombre?.ToLower().Contains(f) ?? false) ||
                (p.ProvinciaNombre?.ToLower().Contains(f) ?? false) ||
                (p.Rol?.ToLower().Contains(f) ?? false);

            if (tabControlPersonas.SelectedTab == tabPageActivos)
            {
                dgvActivos.DataSource = string.IsNullOrWhiteSpace(f) ? _activosCache.ToList() : _activosCache.Where(filtro).ToList();
            }
            else
            {
                dgvInactivos.DataSource = string.IsNullOrWhiteSpace(f) ? _inactivosCache.ToList() : _inactivosCache.Where(filtro).ToList();
            }
        }

        private PersonaDTO? ObtenerSeleccionado(DataGridView dgv)
        {
            return dgv.SelectedRows.Count > 0 ? dgv.SelectedRows[0].DataBoundItem as PersonaDTO : null;
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = new CrearPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarActivos();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvActivos);
            if (persona == null) return;

            using var form = new EditarPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient, persona);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarActivos();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvActivos);
            if (persona == null) return;

            var confirm = MessageBox.Show($"¿Está seguro que desea dar de baja a \"{persona.NombreCompleto}\"?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _personaApiClient.DeleteAsync(persona.IdPersona);
                await CargarTodo();
                MessageBox.Show("Persona dada de baja exitosamente.", "Baja Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dar de baja a la persona: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReactivar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvInactivos);
            if (persona == null) return;

            var confirm = MessageBox.Show($"¿Está seguro que desea reactivar a \"{persona.NombreCompleto}\"?", "Confirmar Reactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                persona.Estado = true;
                await _personaApiClient.UpdateAsync(persona.IdPersona, persona);
                await CargarTodo();
                MessageBox.Show("Persona reactivada exitosamente.", "Reactivación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reactivar a la persona: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

