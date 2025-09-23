using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class PersonaForm : Form
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;
        private List<PersonaDTO> _personasCache = new();
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
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            dgvPersonas.ClearSelection();
            await CargarPersonas();
            AplicarFiltro();
        }

        private async Task CargarPersonas()
        {
            try
            {
                var personas = await _personaApiClient.GetAllAsync() ?? new List<PersonaDTO>();
                _personasCache = personas.ToList();
                dgvPersonas.DataSource = _personasCache.ToList();
                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvPersonas.Columns.Contains("IdPersona"))
                dgvPersonas.Columns["IdPersona"].Visible = false;

            if (dgvPersonas.Columns.Contains("NombreCompleto"))
            {
                var col = dgvPersonas.Columns["NombreCompleto"];
                col.HeaderText = "Nombre Completo";
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.MinimumWidth = 150;
            }

            if (dgvPersonas.Columns.Contains("Dni"))
            {
                dgvPersonas.Columns["Dni"].HeaderText = "DNI";
                dgvPersonas.Columns["Dni"].Width = 100;
            }

            if (dgvPersonas.Columns.Contains("Email"))
            {
                dgvPersonas.Columns["Email"].HeaderText = "Email";
                dgvPersonas.Columns["Email"].Width = 190;
            }

            if (dgvPersonas.Columns.Contains("Password"))
                dgvPersonas.Columns["Password"].Visible = false;

            if (dgvPersonas.Columns.Contains("Telefono"))
            {
                dgvPersonas.Columns["Telefono"].HeaderText = "Teléfono";
                dgvPersonas.Columns["Telefono"].Width = 110;
            }

            if (dgvPersonas.Columns.Contains("Direccion"))
            {
                dgvPersonas.Columns["Direccion"].HeaderText = "Dirección";
                dgvPersonas.Columns["Direccion"].Width = 140;
            }

            if (dgvPersonas.Columns.Contains("IdLocalidad"))
                dgvPersonas.Columns["IdLocalidad"].Visible = false;

            if (dgvPersonas.Columns.Contains("FechaIngreso"))
                dgvPersonas.Columns["FechaIngreso"].Visible = false;

            if (dgvPersonas.Columns.Contains("LocalidadNombre"))
            {
                dgvPersonas.Columns["LocalidadNombre"].HeaderText = "Localidad";
                dgvPersonas.Columns["LocalidadNombre"].Width = 110;
            }

            if (dgvPersonas.Columns.Contains("ProvinciaNombre"))
            {
                dgvPersonas.Columns["ProvinciaNombre"].HeaderText = "Provincia";
                dgvPersonas.Columns["ProvinciaNombre"].Width = 100;
            }

            if (dgvPersonas.Columns.Contains("Rol"))
            {
                dgvPersonas.Columns["Rol"].HeaderText = "Rol";
                dgvPersonas.Columns["Rol"].Width = 80;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var f = _filtroActual.ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(f))
            {
                dgvPersonas.DataSource = _personasCache.ToList();
            }
            else
            {
                dgvPersonas.DataSource = _personasCache
                    .Where(p =>
                        (!string.IsNullOrEmpty(p.NombreCompleto) && p.NombreCompleto.ToLower().Contains(f)) ||
                        p.Dni.ToString().Contains(f) ||
                        (!string.IsNullOrEmpty(p.Email) && p.Email.ToLower().Contains(f)) ||
                        (!string.IsNullOrEmpty(p.Telefono) && p.Telefono.ToLower().Contains(f)) ||
                        (!string.IsNullOrEmpty(p.Direccion) && p.Direccion.ToLower().Contains(f)) ||
                        (!string.IsNullOrEmpty(p.LocalidadNombre) && p.LocalidadNombre.ToLower().Contains(f)) ||
                        (!string.IsNullOrEmpty(p.ProvinciaNombre) && p.ProvinciaNombre.ToLower().Contains(f)) ||
                        (!string.IsNullOrEmpty(p.Rol) && p.Rol.ToLower().Contains(f))
                    ).ToList();
            }

            ConfigurarColumnas();
        }

        private PersonaDTO? ObtenerSeleccionado()
        {
            var row = dgvPersonas.SelectedRows.Count > 0
                ? dgvPersonas.SelectedRows[0]
                : dgvPersonas.CurrentRow;

            return row?.DataBoundItem as PersonaDTO;
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = new CrearPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarPersonas();
                AplicarFiltro();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado();
            if (persona == null) return;

            using var form = new EditarPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient, persona);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarPersonas();
                AplicarFiltro();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado();
            if (persona == null) return;

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea eliminar a \"{persona.NombreCompleto}\"?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                await _personaApiClient.DeleteAsync(persona.IdPersona);
                _personasCache.RemoveAll(p => p.IdPersona == persona.IdPersona);
                AplicarFiltro();

                MessageBox.Show("Persona eliminada exitosamente.",
                    "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error de red: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar persona: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void dgvPersonas_SelectionChanged(object sender, EventArgs e)
        {
            bool seleccionado = dgvPersonas.SelectedRows.Count > 0;
            btnEditar.Visible = seleccionado;
            btnEliminar.Visible = seleccionado;
        }
    }
}
