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
    public partial class DetalleVentaForm : BaseForm
    {
        private readonly int _ventaId;
        private VentaDTO _venta;
        private readonly bool _esAdmin;
        private BindingList<LineaVentaDTO> _lineasDeVenta;
        private readonly VentaApiClient _ventaApiClient;
        private readonly ProductoApiClient _productoApiClient;
        private readonly IServiceProvider _serviceProvider;
        private List<ProductoDTO> _todosLosProductos;
        private bool _datosModificados = false;

        // # CORRECCIÓN: Se elimina LineaVentaApiClient del constructor
        public DetalleVentaForm(
            int ventaId,
            bool esAdmin,
            VentaApiClient ventaApiClient,
            ProductoApiClient productoApiClient,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _ventaId = ventaId;
            _esAdmin = esAdmin;
            _ventaApiClient = ventaApiClient;
            _productoApiClient = productoApiClient;
            _serviceProvider = serviceProvider;

            _todosLosProductos = new List<ProductoDTO>();
            _lineasDeVenta = new BindingList<LineaVentaDTO>();

            this.StartPosition = FormStartPosition.CenterScreen;

            StyleManager.ApplyDataGridViewStyle(dataGridDetalle);
            StyleManager.ApplyButtonStyle(btnEliminarLinea);
            StyleManager.ApplyButtonStyle(btnEditarCantidad);
            StyleManager.ApplyButtonStyle(btnCerrar);
            StyleManager.ApplyButtonStyle(btnAgregarLinea);
            StyleManager.ApplyButtonStyle(btnConfirmarCambios);
        }

        private async void DetalleVentaForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                _venta = await _ventaApiClient.GetByIdAsync(_ventaId);
                if (_venta == null)
                {
                    MessageBox.Show("No se pudieron cargar los datos de la venta.", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                _todosLosProductos = await _productoApiClient.GetAllAsync() ?? new List<ProductoDTO>();

                // # CORRECCIÓN: Las líneas vienen dentro del VentaDTO.
                _lineasDeVenta = new BindingList<LineaVentaDTO>(_venta.Lineas ?? new List<LineaVentaDTO>());

                lblIdVenta.Text = $"ID Venta: {_venta.IdVenta}";
                lblFecha.Text = $"Fecha: {_venta.Fecha:dd/MM/yyyy HH:mm}";
                lblVendedor.Text = $"Vendedor: {_venta.NombreVendedor}";

                ConfigurarVisibilidadControles();
                dataGridDetalle.DataSource = _lineasDeVenta;
                ConfigurarColumnas();
                ActualizarTotal();

                btnConfirmarCambios.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los detalles de la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarVisibilidadControles()
        {
            bool ventaPendiente = "Pendiente".Equals(_venta.Estado, StringComparison.OrdinalIgnoreCase);
            bool puedeEditar = _esAdmin && ventaPendiente;

            btnAgregarLinea.Visible = puedeEditar;
            btnEliminarLinea.Visible = puedeEditar;
            btnConfirmarCambios.Visible = puedeEditar;
            btnEditarCantidad.Visible = puedeEditar;
            dataGridDetalle.ReadOnly = !puedeEditar;

            if (dataGridDetalle.Columns.Contains("Cantidad"))
            {
                dataGridDetalle.Columns["Cantidad"].ReadOnly = !puedeEditar;
            }
        }

        private void ConfigurarColumnas()
        {
            dataGridDetalle.AutoGenerateColumns = false;
            dataGridDetalle.Columns.Clear();
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreProducto", HeaderText = "Producto", DataPropertyName = "NombreProducto", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cantidad", DataPropertyName = "Cantidad" });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrecioUnitario", HeaderText = "Precio Unit.", DataPropertyName = "PrecioUnitario", ReadOnly = true, DefaultCellStyle = { Format = "C2" } });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal", DataPropertyName = "Subtotal", ReadOnly = true, DefaultCellStyle = { Format = "C2" } });
        }

        private void ActualizarTotal()
        {
            _venta.Total = _lineasDeVenta.Sum(l => l.Subtotal);
            lblTotal.Text = $"Total: {_venta.Total:C2}";
        }

        private void MarcarComoModificado()
        {
            _datosModificados = true;
            btnConfirmarCambios.Enabled = true;
        }

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            var idsProductosEnVenta = _lineasDeVenta.Select(l => l.IdProducto).ToList();
            var productosDisponibles = _todosLosProductos
                .Where(p => p.Stock > 0 && !idsProductosEnVenta.Contains(p.IdProducto))
                .ToList();

            if (!productosDisponibles.Any())
            {
                MessageBox.Show("No hay más productos con stock disponibles para agregar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var form = _serviceProvider.GetRequiredService<AñadirProductoVentaForm>();
            form.CargarProductosDisponibles(productosDisponibles);

            if (form.ShowDialog() == DialogResult.OK)
            {
                var productoSeleccionado = form.ProductoSeleccionado;
                var cantidad = form.CantidadSeleccionada;

                var nuevaLinea = new LineaVentaDTO
                {
                    IdVenta = this._venta.IdVenta,
                    IdProducto = productoSeleccionado.IdProducto,
                    NombreProducto = productoSeleccionado.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = (decimal)productoSeleccionado.PrecioActual,
                    EsNueva = true
                };

                _lineasDeVenta.Add(nuevaLinea);
                MarcarComoModificado();
                ActualizarTotal();
            }
        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridDetalle.CurrentRow?.DataBoundItem is not LineaVentaDTO lineaSeleccionada)
            {
                MessageBox.Show("Seleccione la línea de producto que desea eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"¿Está seguro de que desea eliminar '{lineaSeleccionada.NombreProducto}' de la venta?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                _lineasDeVenta.Remove(lineaSeleccionada);
                MarcarComoModificado();
                ActualizarTotal();
            }
        }

        private void btnEditarCantidad_Click(object sender, EventArgs e)
        {
            if (dataGridDetalle.CurrentRow != null && dataGridDetalle.Columns.Contains("Cantidad"))
            {
                dataGridDetalle.CurrentCell = dataGridDetalle.CurrentRow.Cells["Cantidad"];
                dataGridDetalle.BeginEdit(true);
            }
        }

        private async void btnConfirmarCambios_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var dto = new CrearVentaCompletaDTO
                {
                    IdVenta = _venta.IdVenta,
                    IdPersona = _venta.IdPersona.Value,
                    Lineas = _lineasDeVenta.ToList(),
                    Finalizada = false
                };

                var response = await _ventaApiClient.UpdateCompletaAsync(dto);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cambios guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _datosModificados = false;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al guardar los cambios: {error}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (_datosModificados)
            {
                var result = MessageBox.Show("Hay cambios sin guardar. ¿Desea salir de todas formas?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            this.DialogResult = _datosModificados ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }

        private void dataGridDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var lineaEditada = _lineasDeVenta[e.RowIndex];

            if (!int.TryParse(dataGridDetalle.Rows[e.RowIndex].Cells["Cantidad"].Value?.ToString(), out int nuevaCantidad) || nuevaCantidad <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad numérica válida y mayor a cero.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridDetalle.CancelEdit();
                _lineasDeVenta.ResetItem(e.RowIndex);
                return;
            }

            var producto = _todosLosProductos.FirstOrDefault(p => p.IdProducto == lineaEditada.IdProducto);
            if (producto == null) return;

            // # CORRECCIÓN: Usamos la propiedad correcta 'Lineas' del DTO '_venta'.
            int cantidadOriginalEnVenta = _venta.Lineas.FirstOrDefault(l => l.NroLineaVenta == lineaEditada.NroLineaVenta)?.Cantidad ?? 0;
            int stockDisponible = producto.Stock + cantidadOriginalEnVenta;

            if (nuevaCantidad > stockDisponible)
            {
                MessageBox.Show($"Stock insuficiente. Stock total disponible para este producto: {stockDisponible}", "Error de Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridDetalle.CancelEdit();
                _lineasDeVenta.ResetItem(e.RowIndex);
                return;
            }

            lineaEditada.Cantidad = nuevaCantidad;

            MarcarComoModificado();
            _lineasDeVenta.ResetItem(e.RowIndex);
            ActualizarTotal();
        }

        private void dataGridDetalle_SelectionChanged(object sender, EventArgs e)
        {
            bool hayFilaSeleccionada = dataGridDetalle.CurrentRow != null;
            btnEliminarLinea.Enabled = hayFilaSeleccionada;
            btnEditarCantidad.Enabled = hayFilaSeleccionada;
        }
    }
}

