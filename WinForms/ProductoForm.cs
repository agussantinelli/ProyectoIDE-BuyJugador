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
    public partial class ProductoForm : BaseForm
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSessionService _userSessionService; 

        private List<ProductoDTO> _productosActivos = new();
        private List<ProductoDTO> _productosInactivos = new();

        private string _filtroActual = string.Empty;

        public ProductoForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _productoApiClient = _serviceProvider.GetRequiredService<ProductoApiClient>();
            _userSessionService = _serviceProvider.GetRequiredService<UserSessionService>();

            StyleManager.ApplyDataGridViewStyle(dgvActivos);
            StyleManager.ApplyDataGridViewStyle(dgvInactivos);
            StyleManager.ApplyButtonStyle(btnNuevo);
            StyleManager.ApplyButtonStyle(btnEditar);
            StyleManager.ApplyButtonStyle(btnDarBaja);
            StyleManager.ApplyButtonStyle(btnReactivar);
            StyleManager.ApplyButtonStyle(btnReportePrecios);
            StyleManager.ApplyButtonStyle(btnEditarPrecio);
            StyleManager.ApplyButtonStyle(btnVolver);
            StyleManager.ApplyButtonStyle(btnVerProveedores);
        }
        private void cmOpciones_Opening(object sender, CancelEventArgs e)
        {
            bool haySeleccion = (tabControl.SelectedTab == tabActivos && dgvActivos.SelectedRows.Count > 0) ||
                                (tabControl.SelectedTab == tabInactivos && dgvInactivos.SelectedRows.Count > 0);

            mnuVerHistorialPrecios.Enabled = haySeleccion;
            mnuEditarPrecio.Enabled = haySeleccion && _userSessionService.EsAdmin;
        }

        private async void ProductoForm_Load(object sender, EventArgs e)
        {
            await CargarTodosProductos();
            AplicarFiltro();
            ConfigurarVisibilidadControles(); 
        }

        private void ConfigurarVisibilidadControles()
        {
            bool esAdmin = _userSessionService.EsAdmin;

            btnDarBaja.Visible = esAdmin;
            btnReactivar.Visible = esAdmin;
            btnEditarPrecio.Visible = esAdmin; 
            btnReportePrecios.Visible = esAdmin; 

            mnuEditarPrecio.Visible = esAdmin;
            mnuVerHistorialPrecios.Visible = esAdmin;
        }

        private async Task CargarTodosProductos()
        {
            try
            {
                var activosTask = _productoApiClient.GetAllAsync();
                var inactivosTask = _userSessionService.EsAdmin ? _productoApiClient.GetAllInactivosAsync() : Task.FromResult<List<ProductoDTO>?>(new List<ProductoDTO>());

                await Task.WhenAll(activosTask, inactivosTask);

                _productosActivos = activosTask.Result ?? new List<ProductoDTO>();
                _productosInactivos = inactivosTask.Result ?? new List<ProductoDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerProveedores_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (tabControl.SelectedTab == tabActivos) ? dgvActivos : dgvInactivos;
            var producto = ObtenerSeleccionado(dgv);
            if (producto == null) return;

            var existingForm = this.MdiParent?.MdiChildren.OfType<VerProveedoresProductoForm>()
                .FirstOrDefault(f => f.Tag is int prodId && prodId == producto.IdProducto);

            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = _serviceProvider.GetRequiredService<VerProveedoresProductoForm>();
                form.CargarDatos(producto.IdProducto, producto.Nombre);
                form.Tag = producto.IdProducto;
                form.MdiParent = this.MdiParent;
                form.Show();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void cmbFiltroStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string f = _filtroActual.ToLowerInvariant();
            string filtroStock = cmbFiltroStock?.SelectedItem?.ToString() ?? "Todos";

            if (tabControl.SelectedTab == tabActivos)
            {
                var lista = _productosActivos
                    .Where(p => (string.IsNullOrWhiteSpace(f)
                                 || (p.Nombre != null && p.Nombre.ToLower().Contains(f))
                                 || (p.Descripcion != null && p.Descripcion.ToLower().Contains(f)))
                                &&
                                (filtroStock == "Todos"
                                 || (filtroStock == "Con stock" && p.Stock > 0)
                                 || (filtroStock == "Sin stock" && p.Stock == 0)
                                 || (filtroStock == "Stock < 10" && p.Stock < 10)))
                    .ToList();

                ConfigurarColumnas(dgvActivos);
                dgvActivos.DataSource = lista;
            }
            else if (_userSessionService.EsAdmin)
            {
                var lista = _productosInactivos
                    .Where(p => (string.IsNullOrWhiteSpace(f)
                                 || (p.Nombre != null && p.Nombre.ToLower().Contains(f))
                                 || (p.Descripcion != null && p.Descripcion.ToLower().Contains(f)))
                                &&
                                (filtroStock == "Todos"
                                 || (filtroStock == "Con stock" && p.Stock > 0)
                                 || (filtroStock == "Sin stock" && p.Stock == 0)
                                 || (filtroStock == "Stock < 10" && p.Stock < 10)))
                    .ToList();

                ConfigurarColumnas(dgvInactivos);
                dgvInactivos.DataSource = lista;
            }
        }

        private void ConfigurarColumnas(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.AutoGenerateColumns = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdProducto", HeaderText = "ID", Width = 50 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Nombre", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Descripcion", HeaderText = "Descripción", Width = 420 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Stock", HeaderText = "Stock", Width = 70 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TipoProductoDescripcion", HeaderText = "Tipo", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioActual", HeaderText = "Precio", Width = 100, DefaultCellStyle = { Format = "C2" } });
        }

        private ProductoDTO? ObtenerSeleccionado(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
                return dgv.SelectedRows[0].DataBoundItem as ProductoDTO;
            return null;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var existingForm = this.MdiParent?.MdiChildren.OfType<CrearProductoForm>().FirstOrDefault();
            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = _serviceProvider.GetRequiredService<CrearProductoForm>();
                form.MdiParent = this.MdiParent;
                form.FormClosed += async (s, args) =>
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        await CargarTodosProductos();
                        AplicarFiltro();
                    }
                };
                form.Show();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabActivos) return;
            var producto = ObtenerSeleccionado(dgvActivos);
            if (producto == null) return;

            var existingForm = this.MdiParent?.MdiChildren.OfType<EditarProductoForm>()
                .FirstOrDefault(f => f.Tag is int prodId && prodId == producto.IdProducto);

            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = new EditarProductoForm(producto.IdProducto, _productoApiClient, _serviceProvider.GetRequiredService<TipoProductoApiClient>());
                form.Tag = producto.IdProducto;
                form.MdiParent = this.MdiParent;
                form.FormClosed += async (s, args) =>
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        await CargarTodosProductos();
                        AplicarFiltro();
                    }
                };
                form.Show();
            }
        }

        private void btnReportePrecios_Click(object sender, EventArgs e)
        {
            var existingForm = this.MdiParent?.MdiChildren.OfType<ReporteHistorialPreciosForm>().FirstOrDefault();
            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var form = new ReporteHistorialPreciosForm(_serviceProvider.GetRequiredService<PrecioVentaApiClient>(), _serviceProvider.GetRequiredService<ReporteApiClient>());
                form.MdiParent = this.MdiParent;
                form.Show();
            }
        }

        private void btnEditarPrecio_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (tabControl.SelectedTab == tabActivos) ? dgvActivos : dgvInactivos;
            var producto = ObtenerSeleccionado(dgv);
            if (producto == null) return;

            using var edit = new EditarPrecioForm(
                _serviceProvider.GetRequiredService<PrecioVentaApiClient>(),
                producto.IdProducto,
                producto.Nombre,
                producto.PrecioActual);

            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                CargarTodosProductos().ContinueWith(t => AplicarFiltro(), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private async void btnDarBaja_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabActivos) return;
            var producto = ObtenerSeleccionado(dgvActivos);
            if (producto == null) return;

            if (MessageBox.Show($"¿Dar de baja \"{producto.Nombre}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            await _productoApiClient.DeleteAsync(producto.IdProducto);
            await CargarTodosProductos();
            AplicarFiltro();
        }

        private async void btnReactivar_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabInactivos) return;
            var producto = ObtenerSeleccionado(dgvInactivos);
            if (producto == null) return;

            if (MessageBox.Show($"¿Reactivar \"{producto.Nombre}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            await _productoApiClient.ReactivarAsync(producto.IdProducto);
            await CargarTodosProductos();
            AplicarFiltro();
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) => AplicarFiltro();

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            bool seleccionadoActivos = dgvActivos.SelectedRows.Count > 0;
            bool seleccionadoInactivos = dgvInactivos.SelectedRows.Count > 0;
            bool haySeleccion = seleccionadoActivos || seleccionadoInactivos;
            bool esAdmin = _userSessionService.EsAdmin;

            btnEditar.Enabled = (tabControl.SelectedTab == tabActivos) && seleccionadoActivos;
            btnDarBaja.Enabled = (tabControl.SelectedTab == tabActivos) && seleccionadoActivos && esAdmin;
            btnReactivar.Enabled = (tabControl.SelectedTab == tabInactivos) && seleccionadoInactivos && esAdmin;

            btnEditarPrecio.Enabled = haySeleccion && esAdmin;
            btnVerProveedores.Enabled = haySeleccion;

            btnReportePrecios.Enabled = esAdmin;
        }

        private void dgvProductos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                DataGridView dgv = (tabControl.SelectedTab == tabActivos) ? dgvActivos : dgvInactivos;
                dgv.ClearSelection();
                dgv.Rows[e.RowIndex].Selected = true;
                cmOpciones.Show(dgv, e.Location);
            }
        }

        private void AbrirHistorialPreciosProducto(ProductoDTO producto)
        {
            if (producto is null) return;

            var existing = this.MdiParent?.MdiChildren
                .OfType<HistorialPreciosForm>()
                .FirstOrDefault(f => f.Tag is int id && id == producto.IdProducto);

            if (existing != null)
            {
                existing.BringToFront();
                return;
            }

            var api = _serviceProvider.GetRequiredService<PrecioVentaApiClient>();
            var frm = new HistorialPreciosForm(api, producto.IdProducto, producto.Nombre)
            {
                MdiParent = this.MdiParent,
                Tag = producto.IdProducto
            };
            frm.Show();
        }

        private void dgvProductos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridView dgv = ReferenceEquals(sender, dgvActivos) ? dgvActivos : dgvInactivos;
            var prod = ObtenerSeleccionado(dgv);
            if (prod is null) return;

            AbrirHistorialPreciosProducto(prod);
        }


        private void mnuVerHistorialPrecios_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (tabControl.SelectedTab == tabActivos) ? dgvActivos : dgvInactivos;
            var prod = ObtenerSeleccionado(dgv);
            if (prod is null) return;

            AbrirHistorialPreciosProducto(prod);
        }

        private void mnuEditarPrecio_Click(object sender, EventArgs e) => btnEditarPrecio_Click(sender, e);
    }
}

