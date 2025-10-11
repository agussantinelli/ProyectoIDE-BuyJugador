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
    public partial class DetalleVentaForm : BaseForm
    {
        // # Campos privados para manejar el estado interno del formulario.
        private readonly int _ventaId;
        private VentaDTO _venta;
        private readonly bool _esAdmin;

        private BindingList<LineaVentaDTO> _lineasDeVenta;

        // # Inyección de todos los ApiClients que este formulario necesita para ser autónomo.
        private readonly VentaApiClient _ventaApiClient;
        private readonly LineaVentaApiClient _lineaVentaApiClient;
        private readonly ProductoApiClient _productoApiClient;
        private readonly IServiceProvider _serviceProvider;

        private List<ProductoDTO> _todosLosProductos;
        private bool _datosModificados = false;

        // # CONSTRUCTOR REFACTORIZADO:
        // # Ahora recibe el ID de la venta y todos los servicios que necesita para funcionar por sí mismo.
        public DetalleVentaForm(
            int ventaId,
            bool esAdmin,
            VentaApiClient ventaApiClient,
            LineaVentaApiClient lineaVentaApiClient,
            ProductoApiClient productoApiClient,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // # Asignación de valores en el constructor para asegurar que estén disponibles desde el inicio.
            _ventaId = ventaId;
            _esAdmin = esAdmin;
            _ventaApiClient = ventaApiClient;
            _lineaVentaApiClient = lineaVentaApiClient;
            _productoApiClient = productoApiClient;
            _serviceProvider = serviceProvider;

            _todosLosProductos = new List<ProductoDTO>();
            _lineasDeVenta = new BindingList<LineaVentaDTO>();

            this.StartPosition = FormStartPosition.CenterScreen;

            // # Configuración de estilos (sin cambios).
            StyleManager.ApplyDataGridViewStyle(dataGridDetalle);
            StyleManager.ApplyButtonStyle(btnEliminarLinea);
            StyleManager.ApplyButtonStyle(btnEditarCantidad);
            StyleManager.ApplyButtonStyle(btnCerrar);
            StyleManager.ApplyButtonStyle(btnAgregarLinea);
            StyleManager.ApplyButtonStyle(btnConfirmarCambios);
        }

        // # MÉTODO LOAD REFACTORIZADO:
        // # Orquesta la carga de TODOS los datos necesarios para este formulario.
        private async void DetalleVentaForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // # 1. Cargar los datos de la cabecera de la venta usando el ID.
                _venta = await _ventaApiClient.GetByIdAsync(_ventaId);
                if (_venta == null)
                {
                    MessageBox.Show("No se pudieron cargar los datos de la venta.", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // # 2. Cargar la lista completa de productos para usarla al agregar nuevas líneas.
                _todosLosProductos = await _productoApiClient.GetAllAsync() ?? new List<ProductoDTO>();

                // # 3. Cargar las líneas de detalle específicas de ESTA venta.
                var lineas = await _lineaVentaApiClient.GetLineasByVentaIdAsync(_ventaId);
                _lineasDeVenta = new BindingList<LineaVentaDTO>(lineas ?? new List<LineaVentaDTO>());

                // # 4. Poblar la interfaz de usuario con los datos cargados.
                lblIdVenta.Text = $"ID Venta: {_venta.IdVenta}";
                lblFecha.Text = $"Fecha: {_venta.Fecha:dd/MM/yyyy HH:mm}";
                lblVendedor.Text = $"Vendedor: {_venta.NombreVendedor}";

                ConfigurarVisibilidadControles();
                RefrescarGrid();
                ActualizarTotal();

                btnEditarCantidad.Enabled = false;
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

            btnAgregarLinea.Visible = _esAdmin && ventaPendiente;
            btnEliminarLinea.Visible = _esAdmin && ventaPendiente;
            btnConfirmarCambios.Visible = _esAdmin && ventaPendiente;
            btnEditarCantidad.Visible = _esAdmin && ventaPendiente;
            dataGridDetalle.ReadOnly = !_esAdmin || !ventaPendiente;

            if (!dataGridDetalle.ReadOnly && dataGridDetalle.Columns.Contains("Cantidad"))
            {
                dataGridDetalle.Columns["Cantidad"].ReadOnly = false;
            }
        }

        private void ConfigurarColumnas()
        {
            dataGridDetalle.AutoGenerateColumns = false;
            dataGridDetalle.Columns.Clear();

            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreProducto", HeaderText = "Producto", DataPropertyName = "NombreProducto", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cantidad", DataPropertyName = "Cantidad", ReadOnly = !(_esAdmin && "Pendiente".Equals(_venta.Estado, StringComparison.OrdinalIgnoreCase)) });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrecioUnitario", HeaderText = "Precio Unit.", DataPropertyName = "PrecioUnitario", ReadOnly = true, DefaultCellStyle = { Format = "C2" } });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal", DataPropertyName = "Subtotal", ReadOnly = true, DefaultCellStyle = { Format = "C2" } });
        }

        private void RefrescarGrid()
        {
            dataGridDetalle.DataSource = null;
            if (_lineasDeVenta.Any())
            {
                // # El DTO ya trae el Subtotal calculado desde el backend, por lo que podemos enlazarlo directamente.
                dataGridDetalle.DataSource = _lineasDeVenta;
                ConfigurarColumnas();
            }
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
                nuevaLinea.Subtotal = nuevaLinea.Cantidad * nuevaLinea.PrecioUnitario;

                _lineasDeVenta.Add(nuevaLinea);
                MarcarComoModificado();
                RefrescarGrid();
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
                RefrescarGrid();
                ActualizarTotal();
            }
        }

        private void btnEditarCantidad_Click(object sender, EventArgs e)
        {
            if (dataGridDetalle.CurrentRow == null) return;

            dataGridDetalle.CurrentCell = dataGridDetalle.CurrentRow.Cells["Cantidad"];
            dataGridDetalle.BeginEdit(true);
        }

        private async void btnConfirmarCambios_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                for (int i = 0; i < _lineasDeVenta.Count; i++)
                {
                    _lineasDeVenta[i].NroLineaVenta = i + 1;
                }

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

            int cantidadOriginalEnVenta = _venta.Lineas.FirstOrDefault(l => l.IdProducto == lineaEditada.IdProducto)?.Cantidad ?? 0;
            int stockDisponible = producto.Stock + cantidadOriginalEnVenta;

            if (nuevaCantidad > stockDisponible)
            {
                MessageBox.Show($"Stock insuficiente. Stock total disponible para este producto: {stockDisponible}", "Error de Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridDetalle.CancelEdit();
                _lineasDeVenta.ResetItem(e.RowIndex);
                return;
            }

            lineaEditada.Cantidad = nuevaCantidad;
            lineaEditada.Subtotal = nuevaCantidad * lineaEditada.PrecioUnitario;

            MarcarComoModificado();
            _lineasDeVenta.ResetItem(e.RowIndex);
            ActualizarTotal();
        }

        private void dataGridDetalle_SelectionChanged(object sender, EventArgs e)
        {
            bool hayFilaSeleccionada = dataGridDetalle.CurrentRow != null;
            btnEliminarLinea.Enabled = hayFilaSeleccionada && _esAdmin;
            btnEditarCantidad.Enabled = hayFilaSeleccionada && _esAdmin;
        }
    }
}

