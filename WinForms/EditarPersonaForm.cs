using ApiClient;
using DTOs;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic; 


namespace WinForms
{
    public partial class EditarPersonaForm : BaseForm
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly PersonaDTO _persona;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;

        public EditarPersonaForm(
            PersonaApiClient personaApiClient,
            ProvinciaApiClient provinciaApiClient,
            LocalidadApiClient localidadApiClient,
            PersonaDTO persona)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            _provinciaApiClient = provinciaApiClient;
            _localidadApiClient = localidadApiClient;
            _persona = persona ?? throw new ArgumentNullException(nameof(persona));

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void EditarPersonaForm_Load(object sender, EventArgs e)
        {
            txtId.Text = _persona.IdPersona.ToString();
            txtNombreCompleto.Text = _persona.NombreCompleto;
            txtDni.Text = _persona.Dni.ToString();

            txtId.ReadOnly = true;
            txtId.BackColor = Color.LightGray;
            txtNombreCompleto.ReadOnly = true;
            txtNombreCompleto.BackColor = Color.LightGray;
            txtDni.ReadOnly = true;
            txtDni.BackColor = Color.LightGray;

            txtEmail.Text = _persona.Email;
            txtTelefono.Text = _persona.Telefono;
            txtDireccion.Text = _persona.Direccion;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var provincias = await _provinciaApiClient.GetAllAsync() ?? new List<ProvinciaDTO>();
                cmbProvincia.DataSource = provincias;
                cmbProvincia.DisplayMember = "Nombre";
                cmbProvincia.ValueMember = "IdProvincia";

                if (_persona.IdLocalidad.HasValue)
                {
                    var localidadActual = await _localidadApiClient.GetByIdAsync(_persona.IdLocalidad.Value);
                    if (localidadActual != null && localidadActual.IdProvincia.HasValue)
                    {
                        cmbProvincia.SelectedValue = localidadActual.IdProvincia.Value;
                        await CargarLocalidadesAsync(localidadActual.IdProvincia.Value);
                        cmbLocalidad.SelectedValue = localidadActual.IdLocalidad;
                    }
                    else if (provincias.Any()) 
                    {
                        cmbProvincia.SelectedIndex = 0;
                        await CargarLocalidadesAsync(provincias[0].IdProvincia);
                    }
                }
                else if (provincias.Any()) 
                {
                    cmbProvincia.SelectedIndex = 0;
                    await CargarLocalidadesAsync(provincias[0].IdProvincia);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos iniciales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbProvincia.Enabled = false;
                cmbLocalidad.Enabled = false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async Task CargarLocalidadesAsync(int idProvincia)
        {
            cmbLocalidad.DataSource = null;
            cmbLocalidad.Enabled = false;
            if (idProvincia <= 0) return;

            try
            {
                var localidades = await _localidadApiClient.GetAllOrderedAsync();
                var filtradas = localidades?
                    .Where(l => l.IdProvincia == idProvincia)
                    .ToList() ?? new List<LocalidadDTO>();

                cmbLocalidad.DataSource = filtradas;
                cmbLocalidad.DisplayMember = "Nombre";
                cmbLocalidad.ValueMember = "IdLocalidad";
                cmbLocalidad.Enabled = filtradas.Any();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbLocalidad.SelectedValue == null || (int)cmbLocalidad.SelectedValue <= 0)
            {
                MessageBox.Show("Debe seleccionar una localidad válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("El email no puede estar vacío.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("⚠️ Se modificarán los datos de la persona.\n¿Desea continuar?", "Confirmar edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            _persona.Email = txtEmail.Text.Trim();
            _persona.Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim();
            _persona.Direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim();
            _persona.IdLocalidad = (int)cmbLocalidad.SelectedValue;


            this.Cursor = Cursors.WaitCursor;
            try
            {
                var response = await _personaApiClient.UpdateAsync(_persona.IdPersona, _persona);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Persona actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"No se pudo actualizar la persona: {error}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.SelectedValue is int idProvincia)
            {
                this.Cursor = Cursors.WaitCursor;
                await CargarLocalidadesAsync(idProvincia);
                this.Cursor = Cursors.Default;
            }
        }
    }
}
