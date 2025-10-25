using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

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
        private readonly PrecioCompraApiClient _precioCompraApiClient;
        private readonly UserSessionService _userSessionService;

        private BindingList<ProveedorDTO> _activosBindingList = new();
        private BindingList<ProveedorDTO> _inactivosBindingList = new();
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
            _precioCompraApiClient = serviceProvider.GetRequiredService<PrecioCompraApiClient>();
            _userSessionService = serviceProvider.GetRequiredService<UserSessionService>();

            StyleManager.ApplyDataGridViewStyle(dgvActivos);
            StyleManager.ApplyDataGridViewStyle(dgvInactivos);
            StyleManager.ApplyButtonStyle(btnNuevo);
            StyleManager.ApplyButtonStyle(btnEditar);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnReactivar);
            StyleManager.ApplyButtonStyle(btnVolver);
            StyleManager.ApplyButtonStyle(btnAsignarProductos);
            StyleManager.ApplyButtonStyle(btnVerProductos);
        }

        private async void ProveedorForm_Load(object sender, EventArgs e)
        {
            await CargarDatosIniciales();
            AplicarFiltro();
            ActualizarEstadoBotones();   
        }

        private async Task CargarDatosIniciales()
        {
            try
            {
                var activosTask = _proveedorApiClient.GetAllAsync();
                var inactivosTask = _userSessionService.EsAdmin
                    ? _proveedorApiClient.GetInactivosAsync()
                    : Task.FromResult<List<ProveedorDTO>?>(new());
                var provinciasTask = _provinciaApiClient.GetAllAsync();
                var localidadesTask = _localidadApiClient.GetAllAsync();

                await Task.WhenAll(activosTask, inactivosTask, provinciasTask, localidadesTask);

                _activosCache = activosTask.Result ?? new List<ProveedorDTO>();
                _inactivosCache = inactivosTask.Result ?? new List<ProveedorDTO>();
                _provincias = provinciasTask.Result ?? new List<ProvinciaDTO>();
                _localidades = localidadesTask.Result ?? new List<LocalidadDTO>();

                EnriquecerUbicacion(_activosCache, _localidades, _provincias);
                EnriquecerUbicacion(_inactivosCache, _localidades, _provincias);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void EnriquecerUbicacion(List<ProveedorDTO> lista, List<LocalidadDTO> locs, List<ProvinciaDTO> provs)
        {
            foreach (var p in lista)
            {
                var loc = locs.FirstOrDefault(l => l.IdLocalidad == p.IdLocalidad);
                p.LocalidadNombre = loc?.Nombre;
                p.ProvinciaNombre = provs.FirstOrDefault(pr => pr.IdProvincia == (loc?.IdProvincia ?? 0))?.Nombre;
            }
        }

        private void btnVerProductos_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado(dgvActivos);
            if (seleccionado == null) return;

            var existingForm = this.MdiParent?.MdiChildren.OfType<VerProductosProveedorForm>()
                .FirstOrDefault(f => f.Tag is int provId && provId == seleccionado.IdProveedor);

            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = _serviceProvider.GetRequiredService<VerProductosProveedorForm>();
                form.CargarDatos(seleccionado.IdProveedor, seleccionado.RazonSocial);
                form.Tag = seleccionado.IdProveedor;
                form.MdiParent = this.MdiParent;
                form.Show();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var existingForm = this.MdiParent?.MdiChildren.OfType<CrearProveedorForm>().FirstOrDefault();
            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = new CrearProveedorForm(_proveedorApiClient, _provinciaApiClient, _localidadApiClient);
                form.MdiParent = this.MdiParent;
                form.FormClosed += async (s, args) =>
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        await CargarDatosIniciales();
                        AplicarFiltro();
                        ActualizarEstadoBotones();
                    }
                };
                form.Show();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado(dgvActivos);
            if (seleccionado == null) return;

            var existingForm = this.MdiParent?.MdiChildren.OfType<EditarProveedorForm>()
                .FirstOrDefault(f => f.Tag is int provId && provId == seleccionado.IdProveedor);

            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = new EditarProveedorForm(_proveedorApiClient, _provinciaApiClient, _localidadApiClient, seleccionado);
                form.Tag = seleccionado.IdProveedor;
                form.MdiParent = this.MdiParent;
                form.FormClosed += async (s, args) =>
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        await CargarDatosIniciales();
                        AplicarFiltro();
                        ActualizarEstadoBotones();
                    }
                };
                form.Show();
            }
        }

        private void btnAsignarProductos_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado(dgvActivos);
            if (seleccionado == null) return;

            var existingForm = this.MdiParent?.MdiChildren.OfType<AsignarProductosProveedorForm>()
                .FirstOrDefault(f => f.Tag is int provId && provId == seleccionado.IdProveedor);

            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = new AsignarProductosProveedorForm(
                    seleccionado.IdProveedor,
                    seleccionado.RazonSocial,
                    _productoApiClient,
                    _productoProveedorApiClient,
                    _precioCompraApiClient);
                form.Tag = seleccionado.IdProveedor;
                form.MdiParent = this.MdiParent;
                form.Show();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado(dgvActivos);
            if (seleccionado == null) return;

            var confirmResult = MessageBox.Show(
                $"¿Está seguro de que desea dar de baja al proveedor '{seleccionado.RazonSocial}'?",
                "Confirmar Baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    await _proveedorApiClient.DeleteAsync(seleccionado.IdProveedor);
                    MessageBox.Show("Proveedor dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatosIniciales();
                    AplicarFiltro();
                    ActualizarEstadoBotones();
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

            var confirmResult = MessageBox.Show(
                $"¿Está seguro de que desea reactivar al proveedor '{seleccionado.RazonSocial}'?",
                "Confirmar Reactivación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    await _proveedorApiClient.ReactivarAsync(seleccionado.IdProveedor);
                    MessageBox.Show("Proveedor reactivado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatosIniciales();
                    AplicarFiltro();
                    ActualizarEstadoBotones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al reactivar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            ActualizarEstadoBotones();
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private ProveedorDTO? ObtenerSeleccionado(DataGridView dgv)
        {
            if (dgv.CurrentRow?.DataBoundItem is ProveedorDTO p) return p;
            return null;
        }

        private void AplicarFiltro()
        {
            var filtro = _filtroActual.ToLowerInvariant();

            if (tabControlProveedores.SelectedTab == tabPageActivos)
            {
                var filtrados = _activosCache
                    .Where(p =>
                        (p.RazonSocial?.ToLower().Contains(filtro) ?? false) ||
                        (p.Cuit?.Contains(filtro) ?? false))
                    .ToList();

                if (dgvActivos.Columns.Count == 0) ConfigurarColumnas(dgvActivos);
                _activosBindingList = new BindingList<ProveedorDTO>(filtrados);
                dgvActivos.DataSource = _activosBindingList;
            }
            else if (tabControlProveedores.SelectedTab == tabPageInactivos && _userSessionService.EsAdmin)
            {
                var filtrados = _inactivosCache
                    .Where(p =>
                        (p.RazonSocial?.ToLower().Contains(filtro) ?? false) ||
                        (p.Cuit?.Contains(filtro) ?? false))
                    .ToList();

                if (dgvInactivos.Columns.Count == 0) ConfigurarColumnas(dgvInactivos);
                _inactivosBindingList = new BindingList<ProveedorDTO>(filtrados);
                dgvInactivos.DataSource = _inactivosBindingList;
            }
            else if (tabControlProveedores.SelectedTab == tabPageInactivos && !_userSessionService.EsAdmin)
            {
                dgvInactivos.DataSource = null;
            }
        }

        private void ConfigurarColumnas(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;    
            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ProveedorDTO.IdProveedor),HeaderText = "ID", Width = 70});
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ProveedorDTO.RazonSocial), HeaderText = "Razón Social", Width = 180 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ProveedorDTO.Cuit), HeaderText = "CUIT", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ProveedorDTO.Email), HeaderText = "Email", Width = 180 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ProveedorDTO.Telefono), HeaderText = "Teléfono", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ProveedorDTO.Direccion), HeaderText = "Dirección", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ProveedorDTO.LocalidadNombre), HeaderText = "Localidad", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ProveedorDTO.ProvinciaNombre), HeaderText = "Provincia", Width = 150 });

            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
        }

        private void ActualizarEstadoBotones()
        {
            bool esAdmin = _userSessionService.EsAdmin;
            bool esActivosTab = tabControlProveedores.SelectedTab == tabPageActivos;
            bool esInactivosTab = tabControlProveedores.SelectedTab == tabPageInactivos;

            bool hayActivoSeleccionado = esActivosTab && dgvActivos.CurrentRow != null;
            bool hayInactivoSeleccionado = esInactivosTab && dgvInactivos.CurrentRow != null;

            btnNuevo.Visible = esAdmin && esActivosTab;
            btnEditar.Visible = esAdmin && esActivosTab;
            btnEliminar.Visible = esAdmin && esActivosTab;
            btnAsignarProductos.Visible = esActivosTab;
            btnVerProductos.Visible = esActivosTab;
            btnReactivar.Visible = esAdmin && esInactivosTab;

            btnEditar.Enabled = esAdmin && hayActivoSeleccionado && esActivosTab;
            btnEliminar.Enabled = esAdmin && hayActivoSeleccionado && esActivosTab;
            btnAsignarProductos.Enabled = hayActivoSeleccionado && esActivosTab;
            btnVerProductos.Enabled = hayActivoSeleccionado && esActivosTab;
            btnReactivar.Enabled = esAdmin && hayInactivoSeleccionado && esInactivosTab;
        }
    }
}
