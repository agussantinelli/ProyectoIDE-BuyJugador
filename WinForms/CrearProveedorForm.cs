using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic; 


namespace WinForms
{
    public partial class CrearProveedorForm : BaseForm
    {
        private readonly ProveedorApiClient _proveedorApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;

        public CrearProveedorForm(
            ProveedorApiClient proveedorApiClient,
            ProvinciaApiClient provinciaApiClient,
            LocalidadApiClient localidadApiClient)
        {
            InitializeComponent();
            _proveedorApiClient = proveedorApiClient;
            _provinciaApiClient = provinciaApiClient;
            _localidadApiClient = localidadApiClient;

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void CrearProveedorForm_Load(object sender, EventArgs e)
        {
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
                    var localidades = await _localidadApiClient.GetAllOrderedAsync() ?? new List<LocalidadDTO>();
                    var filtradas = localidades.Where(l => l.IdProvincia == idProvincia).ToList();
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
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text) || string.IsNullOrWhiteSpace(txtCuit.Text))
            {
                MessageBox.Show("Ingrese Razón Social y CUIT.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbLocalidad.SelectedValue == null || (int)cmbLocalidad.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar una provincia y localidad.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new ProveedorDTO
            {
                RazonSocial = txtRazonSocial.Text.Trim(),
                Cuit = txtCuit.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                Direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                IdLocalidad = (int)cmbLocalidad.SelectedValue
            };

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var resp = await _proveedorApiClient.CreateAsync(dto);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Proveedor creado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"No se pudo crear el proveedor: {error}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
