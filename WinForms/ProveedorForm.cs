using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class ProveedorForm : BaseForm
    {
        private readonly ProveedorApiClient _proveedorApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;

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

            _proveedorApiClient = serviceProvider.GetRequiredService<ProveedorApiClient>();
            _provinciaApiClient = serviceProvider.GetRequiredService<ProvinciaApiClient>();
            _localidadApiClient = serviceProvider.GetRequiredService<LocalidadApiClient>();

            StyleManager.ApplyDataGridViewStyle(dgvActivos);
            StyleManager.ApplyDataGridViewStyle(dgvInactivos);
            StyleManager.ApplyButtonStyle(btnNuevo);
            StyleManager.ApplyButtonStyle(btnEditar);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnReactivar);
            StyleManager.ApplyButtonStyle(btnVolver);

            this.StartPosition = FormStartPosition.CenterScreen;


            _activosBindingList = new BindingList<ProveedorRow>();
            _inactivosBindingList = new BindingList<ProveedorRow>();

            PrepararGrid(dgvActivos, _activosBindingList);
            PrepararGrid(dgvInactivos, _inactivosBindingList);

            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void PrepararGrid(DataGridView dgv, BindingList<ProveedorRow> bindingList)
        {
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = bindingList;
            dgv.Columns.Clear();
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RazonSocial", HeaderText = "Razón Social", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cuit", HeaderText = "CUIT", Width = 140 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 220 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Telefono", HeaderText = "Teléfono", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Direccion", HeaderText = "Dirección", Width = 170 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LocalidadNombre", HeaderText = "Localidad", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProvinciaNombre", HeaderText = "Provincia", Width = 120 });
        }

        private async void ProveedorForm_Load(object sender, EventArgs e)
        {
            await CargarDatos();
            AplicarFiltro();
            ActualizarVisibilidadBotones();
        }

        private async Task CargarDatos()
        {
            try
            {
                var activos = await _proveedorApiClient.GetAllAsync() ?? new List<ProveedorDTO>();
                var inactivos = await _proveedorApiClient.GetInactivosAsync() ?? new List<ProveedorDTO>();

                _provincias = await _provinciaApiClient.GetAllAsync() ?? new List<ProvinciaDTO>();
                _localidades = await _localidadApiClient.GetAllAsync() ?? new List<LocalidadDTO>();

                _activosCache = activos;
                _inactivosCache = inactivos;

                MapearDatos(dgvActivos, _activosCache, _activosBindingList);
                MapearDatos(dgvInactivos, _inactivosCache, _inactivosBindingList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void MapearDatos(DataGridView dgv, List<ProveedorDTO> cache, BindingList<ProveedorRow> bindingList)
        {
            bindingList.Clear();
            foreach (var p in cache)
            {
                bindingList.Add(ProveedorRow.From(p, _localidades, _provincias));
            }
            dgv.Refresh();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var filtro = _filtroActual.ToLowerInvariant();
            var dgvActual = tabControlProveedores.SelectedTab == tabPageActivos ? dgvActivos : dgvInactivos;
            var bindingListActual = tabControlProveedores.SelectedTab == tabPageActivos ? _activosBindingList : _inactivosBindingList;

            var itemsFiltrados = bindingListActual.Where(row =>
                (row.RazonSocial != null && row.RazonSocial.ToLower().Contains(filtro)) ||
                (row.Cuit != null && row.Cuit.ToLower().Contains(filtro))
            ).ToList();

            dgvActual.DataSource = new BindingList<ProveedorRow>(itemsFiltrados);
        }

        private ProveedorDTO? ObtenerSeleccionado(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0 && dgv.SelectedRows[0].DataBoundItem is ProveedorRow selectedRow)
            {
                var cacheActual = tabControlProveedores.SelectedTab == tabPageActivos ? _activosCache : _inactivosCache;
                return cacheActual.FirstOrDefault(p => p.IdProveedor == selectedRow.IdProveedor);
            }
            return null;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = new CrearProveedorForm(_proveedorApiClient, _provinciaApiClient, _localidadApiClient);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ProveedorForm_Load(this, EventArgs.Empty);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var proveedor = ObtenerSeleccionado(dgvActivos);
            if (proveedor == null) return;

            using var form = new EditarProveedorForm(_proveedorApiClient, _provinciaApiClient, _localidadApiClient, proveedor);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ProveedorForm_Load(this, EventArgs.Empty);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var proveedor = ObtenerSeleccionado(dgvActivos);
            if (proveedor == null) return;

            var confirm = MessageBox.Show($"¿Dar de baja a {proveedor.RazonSocial}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var resp = await _proveedorApiClient.DeleteAsync(proveedor.IdProveedor);
                if (resp.IsSuccessStatusCode)
                {
                    ProveedorForm_Load(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("No se pudo dar de baja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReactivar_Click(object sender, EventArgs e)
        {
            var proveedor = ObtenerSeleccionado(dgvInactivos);
            if (proveedor == null) return;

            var confirm = MessageBox.Show($"¿Reactivar a {proveedor.RazonSocial}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var resp = await _proveedorApiClient.ReactivarAsync(proveedor.IdProveedor);
                if (resp.IsSuccessStatusCode)
                {
                    ProveedorForm_Load(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("No se pudo reactivar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void tabControlProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
            ActualizarVisibilidadBotones();
        }

        private void dgvActivos_SelectionChanged(object sender, EventArgs e) => ActualizarVisibilidadBotones();
        private void dgvInactivos_SelectionChanged(object sender, EventArgs e) => ActualizarVisibilidadBotones();

        private void ActualizarVisibilidadBotones()
        {
            bool esActivos = tabControlProveedores.SelectedTab == tabPageActivos;
            btnEditar.Visible = esActivos && ObtenerSeleccionado(dgvActivos) != null;
            btnEliminar.Visible = esActivos && ObtenerSeleccionado(dgvActivos) != null;
            btnReactivar.Visible = !esActivos && ObtenerSeleccionado(dgvInactivos) != null;
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

