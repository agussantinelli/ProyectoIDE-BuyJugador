using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class ProveedorForm : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProveedorApiClient _proveedorApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;
        private readonly ProductoApiClient _productoApiClient;
        private readonly ProductoProveedorApiClient _productoProveedorApiClient;


        private BindingList<ProveedorRow> _activosBindingList;
        private BindingList<ProveedorRow> _inactivosBindingList;
        private List<ProveedorDTO> _activosCache = new();
        private List<ProveedorDTO> _inactivosCache = new();
        private List<ProvinciaDTO> _provincias = new();
        private List<LocalidadDTO> _localidades = new();
        private string _filtroActual = string.Empty;

        public ProveedorForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _proveedorApiClient = serviceProvider.GetRequiredService<ProveedorApiClient>();
            _provinciaApiClient = serviceProvider.GetRequiredService<ProvinciaApiClient>();
            _localidadApiClient = serviceProvider.GetRequiredService<LocalidadApiClient>();
            _productoApiClient = serviceProvider.GetRequiredService<ProductoApiClient>();
            _productoProveedorApiClient = serviceProvider.GetRequiredService<ProductoProveedorApiClient>();

            StyleManager.ApplyDataGridViewStyle(dgvActivos);
            StyleManager.ApplyDataGridViewStyle(dgvInactivos);
            StyleManager.ApplyButtonStyle(btnNuevo);
            StyleManager.ApplyButtonStyle(btnEditar);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnReactivar);
            StyleManager.ApplyButtonStyle(btnVolver);
            StyleManager.ApplyButtonStyle(btnAsignarProductos);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private async void ProveedorForm_Load(object sender, EventArgs e)
        {
            await CargarDatosIniciales();
            AplicarFiltro();
            ActualizarBotones();
        }

        private async Task CargarDatosIniciales()
        {
            try
            {
                var activosTask = _proveedorApiClient.GetAllAsync();
                var inactivosTask = _proveedorApiClient.GetInactivosAsync();
                var provinciasTask = _provinciaApiClient.GetAllAsync();
                var localidadesTask = _localidadApiClient.GetAllAsync();

                await Task.WhenAll(activosTask, inactivosTask, provinciasTask, localidadesTask);

                _activosCache = activosTask.Result ?? new List<ProveedorDTO>();
                _inactivosCache = inactivosTask.Result ?? new List<ProveedorDTO>();
                _provincias = provinciasTask.Result ?? new List<ProvinciaDTO>();
                _localidades = localidadesTask.Result ?? new List<LocalidadDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = new CrearProveedorForm(
                _proveedorApiClient,
                _provinciaApiClient,
                _localidadApiClient
            );

            if (form.ShowDialog() == DialogResult.OK)
            {
                ProveedorForm_Load(this, EventArgs.Empty);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado(dgvActivos);
            if (seleccionado == null) return;

            var proveedorDto = _activosCache.FirstOrDefault(p => p.IdProveedor == seleccionado.IdProveedor);
            if (proveedorDto == null) return;

            using var form = new EditarProveedorForm(
                _proveedorApiClient,
                _provinciaApiClient,
                _localidadApiClient,
                proveedorDto
            );

            if (form.ShowDialog() == DialogResult.OK)
            {
                ProveedorForm_Load(this, EventArgs.Empty);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado(dgvActivos);
            if (seleccionado == null) return;

            var confirmResult = MessageBox.Show($"¿Está seguro de que desea dar de baja al proveedor '{seleccionado.RazonSocial}'?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    await _proveedorApiClient.DeleteAsync(seleccionado.IdProveedor);
                    MessageBox.Show("Proveedor dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ProveedorForm_Load(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al dar de baja: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnReactivar_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado(dgvInactivos);
            if (seleccionado == null) return;

            var confirmResult = MessageBox.Show($"¿Está seguro de que desea reactivar al proveedor '{seleccionado.RazonSocial}'?", "Confirmar Reactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    await _proveedorApiClient.ReactivarAsync(seleccionado.IdProveedor);
                    MessageBox.Show("Proveedor reactivado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ProveedorForm_Load(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al reactivar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAsignarProductos_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado(dgvActivos);
            if (seleccionado == null) return;

            using var form = new AsignarProductosProveedorForm(
                seleccionado.IdProveedor,
                seleccionado.RazonSocial,
                _productoApiClient,
                _productoProveedorApiClient
            );
            form.ShowDialog();
        }

        private void btnVolver_Click(object sender, EventArgs e) => this.Close();
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text;
            AplicarFiltro();
        }

        private void tabControlProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            ActualizarBotones();
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private ProveedorRow? ObtenerSeleccionado(DataGridView dgv)
        {
            if (dgv.CurrentRow != null && dgv.CurrentRow.DataBoundItem is ProveedorRow proveedor)
            {
                return proveedor;
            }
            MessageBox.Show("Por favor, seleccione un proveedor.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        private void AplicarFiltro()
        {
            var filtro = _filtroActual.ToLower();

            if (tabControlProveedores.SelectedTab == tabPageActivos)
            {
                var filtrados = _activosCache
                    .Where(p => p.RazonSocial.ToLower().Contains(filtro) || p.Cuit.Contains(filtro))
                    .Select(p => ProveedorRow.From(p, _localidades, _provincias))
                    .ToList();

                _activosBindingList = new BindingList<ProveedorRow>(filtrados);
                dgvActivos.DataSource = _activosBindingList;
                ConfigurarColumnas(dgvActivos);
            }
            else
            {
                var filtrados = _inactivosCache
                    .Where(p => p.RazonSocial.ToLower().Contains(filtro) || p.Cuit.Contains(filtro))
                    .Select(p => ProveedorRow.From(p, _localidades, _provincias))
                    .ToList();

                _inactivosBindingList = new BindingList<ProveedorRow>(filtrados);
                dgvInactivos.DataSource = _inactivosBindingList;
                ConfigurarColumnas(dgvInactivos);
            }
        }

        private void ConfigurarColumnas(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProveedorRow.RazonSocial),
                HeaderText = "Razón Social",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProveedorRow.Cuit),
                HeaderText = "CUIT",
                Width = 120
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProveedorRow.Email),
                HeaderText = "Email",
                Width = 180
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProveedorRow.Telefono),
                HeaderText = "Teléfono",
                Width = 120
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProveedorRow.Direccion),
                HeaderText = "Dirección",
                Width = 200
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProveedorRow.LocalidadNombre),
                HeaderText = "Localidad",
                Width = 150
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProveedorRow.ProvinciaNombre),
                HeaderText = "Provincia",
                Width = 150
            });

            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
        }


        private void ActualizarBotones()
        {
            bool esActivos = tabControlProveedores.SelectedTab == tabPageActivos;
            btnEditar.Visible = esActivos && dgvActivos.CurrentRow != null;
            btnEliminar.Visible = esActivos && dgvActivos.CurrentRow != null;
            btnReactivar.Visible = !esActivos && dgvInactivos.CurrentRow != null;
            btnAsignarProductos.Visible = esActivos && dgvActivos.CurrentRow != null;
        }

        private class ProveedorRow
        {
            public int IdProveedor { get; set; }
            public string RazonSocial { get; set; }
            public string Cuit { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
            public string LocalidadNombre { get; set; }
            public string ProvinciaNombre { get; set; }

            public static ProveedorRow From(ProveedorDTO p, List<LocalidadDTO> locs, List<ProvinciaDTO> provs)
            {
                var loc = locs.FirstOrDefault(l => l.IdLocalidad == p.IdLocalidad);
                var prov = provs.FirstOrDefault(x => x.IdProvincia == (loc?.IdProvincia ?? 0));
                return new ProveedorRow
                {
                    IdProveedor = p.IdProveedor,
                    RazonSocial = p.RazonSocial,
                    Cuit = p.Cuit,
                    Email = p.Email,
                    Telefono = p.Telefono,
                    Direccion = p.Direccion,
                    LocalidadNombre = loc?.Nombre,
                    ProvinciaNombre = prov?.Nombre
                };
            }
        }
    }
}

