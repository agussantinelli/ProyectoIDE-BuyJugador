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
    public partial class ProveedorForm : Form
    {
        private readonly ProveedorApiClient _proveedorApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;

        // --- CAMBIO CLAVE: Usaremos BindingList para la vinculación de datos automática ---
        private BindingList<ProveedorRow> _activosBindingList;
        private BindingList<ProveedorRow> _inactivosBindingList;

        // Mantenemos las listas caché para los datos originales de la API
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

            // --- CAMBIO CLAVE: Inicializar BindingLists y vincularlos UNA SOLA VEZ a las grillas ---
            _activosBindingList = new BindingList<ProveedorRow>();
            _inactivosBindingList = new BindingList<ProveedorRow>();
            dgvActivos.DataSource = _activosBindingList;
            dgvInactivos.DataSource = _inactivosBindingList;

            PrepararGrid(dgvActivos);
            PrepararGrid(dgvInactivos);

            StartPosition = FormStartPosition.CenterParent;
        }

        private void PrepararGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;

            AddTextCol(dgv, "RazonSocial", "Razón Social", fill: true, minWidth: 200);
            AddTextCol(dgv, "Cuit", "CUIT", width: 120);
            AddTextCol(dgv, "Email", "Email", width: 200);
            AddTextCol(dgv, "Telefono", "Teléfono", width: 120);
            AddTextCol(dgv, "Direccion", "Dirección", width: 200);
            AddTextCol(dgv, "LocalidadNombre", "Localidad", width: 140);
            AddTextCol(dgv, "ProvinciaNombre", "Provincia", width: 140);
        }

        private static void AddTextCol(DataGridView dgv, string dataProperty, string header,
            int width = 100, bool fill = false, int minWidth = 50)
        {
            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                Name = dataProperty,
                HeaderText = header,
                ReadOnly = true
            };

            if (fill)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.MinimumWidth = minWidth;
            }
            else
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Width = width;
            }

            dgv.Columns.Add(col);
        }

        private async void ProveedorForm_Load(object sender, EventArgs e)
        {
            await CargarTodo();
            ActualizarVisibilidadBotones();
        }

        private async System.Threading.Tasks.Task CargarTodo()
        {
            _provincias = await _provinciaApiClient.GetAllAsync() ?? new();
            _localidades = await _localidadApiClient.GetAllAsync() ?? new();

            // Cargar los datos más recientes en las listas caché
            _activosCache = await _proveedorApiClient.GetAllAsync() ?? new();
            _inactivosCache = await _proveedorApiClient.GetInactivosAsync() ?? new();

            // Actualizar las grillas a través de los BindingLists
            AplicarFiltro();
        }

        // --- CAMBIO CLAVE: Este método ahora manipula los BindingList, no el DataSource ---
        private void AplicarFiltro()
        {
            // Desactivar notificaciones para mejorar el rendimiento durante la carga masiva
            _activosBindingList.RaiseListChangedEvents = false;
            _inactivosBindingList.RaiseListChangedEvents = false;

            _activosBindingList.Clear();
            _inactivosBindingList.Clear();

            var f = _filtroActual.ToLowerInvariant();

            bool Coincide(ProveedorRow p) =>
                (p.RazonSocial?.ToLower().Contains(f) ?? false) ||
                (p.Cuit?.ToLower().Contains(f) ?? false) ||
                (p.Email?.ToLower().Contains(f) ?? false) ||
                (p.Telefono?.ToLower().Contains(f) ?? false) ||
                (p.Direccion?.ToLower().Contains(f) ?? false) ||
                (p.LocalidadNombre?.ToLower().Contains(f) ?? false) ||
                (p.ProvinciaNombre?.ToLower().Contains(f) ?? false);

            _activosCache
                .Select(p => ProveedorRow.From(p, _localidades, _provincias))
                .Where(Coincide)
                .ToList()
                .ForEach(p => _activosBindingList.Add(p));

            _inactivosCache
                .Select(p => ProveedorRow.From(p, _localidades, _provincias))
                .Where(Coincide)
                .ToList()
                .ForEach(p => _inactivosBindingList.Add(p));

            // Reactivar notificaciones para que la UI se actualice
            _activosBindingList.RaiseListChangedEvents = true;
            _inactivosBindingList.RaiseListChangedEvents = true;
            _activosBindingList.ResetBindings();
            _inactivosBindingList.ResetBindings();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private ProveedorRow? ObtenerSeleccionado(DataGridView dgv)
            => dgv.SelectedRows.Count > 0 ? dgv.SelectedRows[0].DataBoundItem as ProveedorRow : null;

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = new CrearProveedorForm(_proveedorApiClient, _provinciaApiClient, _localidadApiClient);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                // Solo recargamos lo que cambió y aplicamos filtro
                _activosCache = await _proveedorApiClient.GetAllAsync() ?? new();
                AplicarFiltro();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            var sel = ObtenerSeleccionado(dgvActivos);
            if (sel == null) return;

            var dto = await _proveedorApiClient.GetByIdAsync(sel.IdProveedor);
            if (dto == null) return;

            using var form = new EditarProveedorForm(_proveedorApiClient, _provinciaApiClient, _localidadApiClient, dto);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _activosCache = await _proveedorApiClient.GetAllAsync() ?? new();
                AplicarFiltro();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var sel = ObtenerSeleccionado(dgvActivos);
            if (sel == null) return;

            var confirm = MessageBox.Show(
                $"¿Dar de baja al proveedor \"{sel.RazonSocial}\"?",
                "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            await _proveedorApiClient.DeleteAsync(sel.IdProveedor);
            await CargarTodo(); // Carga todo de nuevo
        }

        private async void btnReactivar_Click(object sender, EventArgs e)
        {
            var sel = ObtenerSeleccionado(dgvInactivos);
            if (sel == null) return;

            var confirm = MessageBox.Show(
                $"¿Reactivar al proveedor \"{sel.RazonSocial}\"?",
                "Confirmar Reactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            await _proveedorApiClient.ReactivarAsync(sel.IdProveedor);
            await CargarTodo(); // Carga todo de nuevo
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();

        private void dgvActivos_SelectionChanged(object sender, EventArgs e) => ActualizarVisibilidadBotones();
        private void dgvInactivos_SelectionChanged(object sender, EventArgs e) => ActualizarVisibilidadBotones();

        private void tabControlProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadBotones();
        }

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

