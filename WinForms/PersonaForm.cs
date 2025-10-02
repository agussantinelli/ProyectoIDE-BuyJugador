using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class PersonaForm : BaseForm
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

            // Aplicar estilos
            StyleManager.ApplyDataGridViewStyle(dgvActivos);
            StyleManager.ApplyDataGridViewStyle(dgvInactivos);
            StyleManager.ApplyButtonStyle(btnNuevo);
            StyleManager.ApplyButtonStyle(btnEditar);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnReactivar);
            StyleManager.ApplyButtonStyle(btnVolver);


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
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreCompleto", HeaderText = "Nombre", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Dni", HeaderText = "DNI", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Telefono", HeaderText = "Teléfono", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Direccion", HeaderText = "Dirección", Width = 250 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaIngreso", HeaderText = "Fecha Ingreso", Width = 120, DefaultCellStyle = { Format = "yyyy-MM-dd" } });
        }


        private async void PersonaForm_Load(object sender, EventArgs e)
        {
            await CargarPersonas();
            AplicarFiltro();
            ActualizarVisibilidadBotones();
        }

        private async Task CargarPersonas()
        {
            try
            {
                var personas = await _personaApiClient.GetAllAsync() ?? new List<PersonaDTO>();
                _activosCache = personas.Where(p => p.Estado).ToList();
                _inactivosCache = personas.Where(p => !p.Estado).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personal: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var filtro = _filtroActual.ToLowerInvariant();
            var dgvActual = tabControlPersonas.SelectedTab == tabPageActivos ? dgvActivos : dgvInactivos;
            var cacheActual = tabControlPersonas.SelectedTab == tabPageActivos ? _activosCache : _inactivosCache;

            if (string.IsNullOrWhiteSpace(filtro))
            {
                dgvActual.DataSource = cacheActual.ToList();
            }
            else
            {
                dgvActual.DataSource = cacheActual
                    .Where(p => (p.NombreCompleto != null && p.NombreCompleto.ToLower().Contains(filtro)) ||
                                (p.Dni.ToString() != null && p.Dni.ToString().ToLower().Contains(filtro)))
                    .ToList();
            }
        }

        private PersonaDTO? ObtenerSeleccionado(DataGridView dgv)
        {
            return dgv.SelectedRows.Count > 0
                ? dgv.SelectedRows[0].DataBoundItem as PersonaDTO
                : null;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = new CrearPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                PersonaForm_Load(this, EventArgs.Empty);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvActivos);
            if (persona == null) return;

            using var form = new EditarPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient, persona);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                PersonaForm_Load(this, EventArgs.Empty);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvActivos);
            if (persona == null) return;

            var confirm = MessageBox.Show(
                $"¿Dar de baja a {persona.NombreCompleto}?",
                "Confirmar baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var resp = await _personaApiClient.DeleteAsync(persona.IdPersona);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Persona dada de baja.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PersonaForm_Load(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("No se pudo dar de baja a la persona.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dar de baja: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReactivar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvInactivos);
            if (persona == null) return;

            var confirm = MessageBox.Show(
                $"¿Desea reactivar a {persona.NombreCompleto}?",
                "Confirmar Reactivación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var resp = await _personaApiClient.ReactivarAsync(persona.IdPersona);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Persona reactivada exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PersonaForm_Load(this, EventArgs.Empty);
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

