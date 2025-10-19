using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class PersonaForm : BaseForm
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;
        private readonly IServiceProvider _serviceProvider; 

        private List<PersonaDTO> _activosCache = new();
        private List<PersonaDTO> _inactivosCache = new();
        private string _filtroActual = string.Empty;

        public PersonaForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider; 
            _personaApiClient = serviceProvider.GetRequiredService<PersonaApiClient>();
            _provinciaApiClient = serviceProvider.GetRequiredService<ProvinciaApiClient>();
            _localidadApiClient = serviceProvider.GetRequiredService<LocalidadApiClient>();

            StyleManager.ApplyDataGridViewStyle(dgvActivos);
            StyleManager.ApplyDataGridViewStyle(dgvInactivos);
            StyleManager.ApplyButtonStyle(btnNuevo);
            StyleManager.ApplyButtonStyle(btnEditar);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnVerVentas);
            StyleManager.ApplyButtonStyle(btnReactivar);
            StyleManager.ApplyButtonStyle(btnVolver);

            PrepararGrid(dgvActivos);
            PrepararGrid(dgvInactivos);
        }

        private void PrepararGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreCompleto", HeaderText = "Nombre", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Dni", HeaderText = "DNI", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Telefono", HeaderText = "Teléfono", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Direccion", HeaderText = "Dirección", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaIngresoFormateada", HeaderText = "Fecha Ingreso", Width = 130 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LocalidadNombre", HeaderText = "Localidad", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProvinciaNombre", HeaderText = "Provincia", Width = 100 });
        }

        private async void PersonaForm_Load(object sender, EventArgs e)
        {
            await CargarYMostrarDatos();
        }

        private async Task CargarYMostrarDatos()
        {
            try
            {
                _activosCache = await _personaApiClient.GetAllAsync() ?? new List<PersonaDTO>();
                _inactivosCache = await _personaApiClient.GetInactivosAsync() ?? new List<PersonaDTO>();

                AplicarFiltro();
                ActualizarVisibilidadBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el personal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var esTabActivos = tabControlPersonas.SelectedTab == tabPageActivos;
            var dgvActual = esTabActivos ? dgvActivos : dgvInactivos;
            var cacheActual = esTabActivos ? _activosCache : _inactivosCache;

            dgvActual.DataSource = null;

            List<PersonaDTO> datosFiltrados;
            if (string.IsNullOrWhiteSpace(filtro))
            {
                datosFiltrados = cacheActual;
            }
            else
            {
                datosFiltrados = cacheActual
                    .Where(p => (p.NombreCompleto != null && p.NombreCompleto.ToLowerInvariant().Contains(filtro)) ||
                                (p.Dni.ToString().Contains(filtro)))
                    .ToList();
            }

            var bindingSource = new BindingSource { DataSource = datosFiltrados };
            dgvActual.DataSource = bindingSource;
        }

        private PersonaDTO? ObtenerSeleccionado(DataGridView dgv)
        {
            return dgv.SelectedRows.Count > 0 ? dgv.SelectedRows[0].DataBoundItem as PersonaDTO : null;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var existingForm = this.MdiParent?.MdiChildren.OfType<CrearPersonaForm>().FirstOrDefault();
            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = new CrearPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient);
                form.MdiParent = this.MdiParent;
                form.FormClosed += async (s, args) => {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        await CargarYMostrarDatos();
                    }
                };
                form.Show();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvActivos);
            if (persona == null) return;

            var existingForm = this.MdiParent?.MdiChildren.OfType<EditarPersonaForm>()
                .FirstOrDefault(f => f.Tag is int personaId && personaId == persona.IdPersona);

            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = new EditarPersonaForm(_personaApiClient, _provinciaApiClient, _localidadApiClient, persona);
                form.Tag = persona.IdPersona;
                form.MdiParent = this.MdiParent;
                form.FormClosed += async (s, args) => {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        await CargarYMostrarDatos();
                    }
                };
                form.Show();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvActivos);
            if (persona == null) return;

            var confirm = MessageBox.Show($"¿Desea dar de baja a {persona.NombreCompleto}?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var resp = await _personaApiClient.DeleteAsync(persona.IdPersona);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Persona dada de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarYMostrarDatos();
                }
                else
                {
                    MessageBox.Show("No se pudo dar de baja a la persona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dar de baja: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReactivar_Click(object sender, EventArgs e)
        {
            var persona = ObtenerSeleccionado(dgvInactivos);
            if (persona == null) return;

            var confirm = MessageBox.Show($"¿Desea reactivar a {persona.NombreCompleto}?", "Confirmar Reactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var resp = await _personaApiClient.ReactivarAsync(persona.IdPersona);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Persona reactivada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarYMostrarDatos();
                }
                else
                {
                    MessageBox.Show("No se pudo reactivar la persona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reactivar a la persona: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void btnVerVentas_Click(object sender, EventArgs e)
        {
            var esTabActivos = tabControlPersonas.SelectedTab == tabPageActivos;
            var persona = esTabActivos ? ObtenerSeleccionado(dgvActivos) : ObtenerSeleccionado(dgvInactivos);
            if (persona == null)
            {
                MessageBox.Show("Seleccione una persona.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ventaApi = _serviceProvider.GetRequiredService<VentaApiClient>();
            var form = new VentasPersonaForm(ventaApi, persona, _serviceProvider)
            {
                MdiParent = this.MdiParent
            };
            form.Show();
        }

        private void ActualizarVisibilidadBotones()
        {
            bool esTabActivos = tabControlPersonas.SelectedTab == tabPageActivos;
            bool activoSeleccionado = ObtenerSeleccionado(dgvActivos) != null;
            bool inactivoSeleccionado = ObtenerSeleccionado(dgvInactivos) != null;

            btnEditar.Visible = esTabActivos && activoSeleccionado;
            btnEliminar.Visible = esTabActivos && activoSeleccionado;
            btnReactivar.Visible = !esTabActivos && inactivoSeleccionado;
            btnVerVentas.Visible = activoSeleccionado || inactivoSeleccionado;
            btnVerVentas.Enabled = btnVerVentas.Visible;
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

