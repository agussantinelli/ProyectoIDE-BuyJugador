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
    public partial class EditarProveedorForm : BaseForm
    {
        private readonly ProveedorApiClient _proveedorApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;
        private readonly ProveedorDTO _proveedor;

        public EditarProveedorForm(
            ProveedorApiClient proveedorApiClient,
            ProvinciaApiClient provinciaApiClient,
            LocalidadApiClient localidadApiClient,
            ProveedorDTO proveedor)
        {
            InitializeComponent();
            _proveedorApiClient = proveedorApiClient;
            _provinciaApiClient = provinciaApiClient;
            _localidadApiClient = localidadApiClient;
            _proveedor = proveedor ?? throw new ArgumentNullException(nameof(proveedor));

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void EditarProveedorForm_Load(object sender, EventArgs e)
        {
            txtId.Text = _proveedor.IdProveedor.ToString();
            txtRazonSocial.Text = _proveedor.RazonSocial;
            txtCuit.Text = _proveedor.Cuit;

            txtId.ReadOnly = true;
            txtId.BackColor = Color.LightGray;
            txtRazonSocial.ReadOnly = true;
            txtRazonSocial.BackColor = Color.LightGray;
            txtCuit.ReadOnly = true;
            txtCuit.BackColor = Color.LightGray;

            txtEmail.Text = _proveedor.Email;
            txtTelefono.Text = _proveedor.Telefono;
            txtDireccion.Text = _proveedor.Direccion;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var provincias = await _provinciaApiClient.GetAllAsync() ?? new List<ProvinciaDTO>();
                cmbProvincia.DataSource = provincias;
                cmbProvincia.DisplayMember = "Nombre";
                cmbProvincia.ValueMember = "IdProvincia";

                if (_proveedor.IdLocalidad.HasValue)
                {
                    var localidadActual = await _localidadApiClient.GetByIdAsync(_proveedor.IdLocalidad.Value);
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
                var filtradas = localidades?.Where(l => l.IdProvincia == idProvincia).ToList() ?? new List<LocalidadDTO>();
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

        private async void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.SelectedValue is int idProvincia)
            {
                this.Cursor = Cursors.WaitCursor;
                await CargarLocalidadesAsync(idProvincia);
                this.Cursor = Cursors.Default;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbLocalidad.SelectedValue == null || (int)cmbLocalidad.SelectedValue <= 0)
            {
                MessageBox.Show("Debe seleccionar una localidad válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("⚠️ Se modificarán los datos del proveedor.\n¿Desea continuar?", "Confirmar edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            _proveedor.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
            _proveedor.Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim();
            _proveedor.Direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim();
            _proveedor.IdLocalidad = (int)cmbLocalidad.SelectedValue;


            this.Cursor = Cursors.WaitCursor;
            try
            {
                var resp = await _proveedorApiClient.UpdateAsync(_proveedor.IdProveedor, _proveedor);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Proveedor actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"No se pudo actualizar el proveedor: {error}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void txtRazonSocial_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
