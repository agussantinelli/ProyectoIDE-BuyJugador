using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;


namespace WinForms
{
    public partial class CrearPersonaForm : BaseForm
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;

        public CrearPersonaForm(
            PersonaApiClient personaApiClient,
            ProvinciaApiClient provinciaApiClient,
            LocalidadApiClient localidadApiClient)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            _provinciaApiClient = provinciaApiClient;
            _localidadApiClient = localidadApiClient;

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void CrearPersonaForm_Load(object sender, EventArgs e)
        {
            cmbRol.Items.Add("Dueño");
            cmbRol.Items.Add("Empleado");
            cmbRol.SelectedIndex = 1;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var provincias = await _provinciaApiClient.GetAllAsync() ?? new List<ProvinciaDTO>();
                provincias.Insert(0, new ProvinciaDTO { IdProvincia = 0, Nombre = "-- Seleccionar provincia --" });
                cmbProvincia.DataSource = provincias;
                cmbProvincia.DisplayMember = "Nombre";
                cmbProvincia.ValueMember = "IdProvincia";
                cmbProvincia.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar provincias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbProvincia.DataSource = new List<ProvinciaDTO> { new ProvinciaDTO { IdProvincia = 0, Nombre = "-- Error al cargar --" } };
                cmbProvincia.SelectedIndex = 0;
                cmbProvincia.Enabled = false;
                cmbLocalidad.Enabled = false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.SelectedValue is int idProvincia && idProvincia > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                cmbLocalidad.DataSource = null;
                cmbLocalidad.Enabled = false;
                try
                {
                    var localidades = await _localidadApiClient.GetAllOrderedAsync();
                    var filtradas = localidades?
                        .Where(l => l.IdProvincia == idProvincia)
                        .ToList() ?? new List<LocalidadDTO>();
                    filtradas.Insert(0, new LocalidadDTO { IdLocalidad = 0, Nombre = "-- Seleccione una localidad --" });
                    cmbLocalidad.DataSource = filtradas;
                    cmbLocalidad.DisplayMember = "Nombre";
                    cmbLocalidad.ValueMember = "IdLocalidad";
                    cmbLocalidad.SelectedIndex = 0;
                    cmbLocalidad.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar localidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbLocalidad.DataSource = new List<LocalidadDTO> { new LocalidadDTO { IdLocalidad = 0, Nombre = "-- Error al cargar --" } };
                    cmbLocalidad.SelectedIndex = 0;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                cmbLocalidad.DataSource = null;
                cmbLocalidad.Enabled = false;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                string.IsNullOrWhiteSpace(txtDni.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Debe ingresar nombre completo, DNI, email y contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtDni.Text.Trim(), out int dni))
            {
                MessageBox.Show("El DNI debe ser un número válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbLocalidad.SelectedValue == null || (int)cmbLocalidad.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar una provincia y localidad.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var persona = new PersonaDTO
            {
                NombreCompleto = txtNombreCompleto.Text.Trim(),
                Dni = dni,
                Email = txtEmail.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                Direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                IdLocalidad = (int)cmbLocalidad.SelectedValue,
                FechaIngreso = cmbRol.SelectedItem?.ToString() == "Empleado" ? DateOnly.FromDateTime(DateTime.Today) : null
            };

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var response = await _personaApiClient.CreateAsync(persona);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Persona creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al crear la persona: {error}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
