using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WinForms
{
    public partial class DetallePedidoForm : BaseForm
    {
        private readonly int _pedidoId;
        private PedidoDTO _pedido;
        private readonly bool _esAdmin;
        private BindingList<LineaPedidoDTO> _lineasDePedido;
        private readonly PedidoApiClient _pedidoApiClient;
        private readonly ProductoApiClient _productoApiClient;
        private readonly IServiceProvider _serviceProvider;
        private bool _datosModificados = false;
        private List<ProductoDTO> _productosDelProveedor;

        public DetallePedidoForm(
            int pedidoId,
            bool esAdmin,
            PedidoApiClient pedidoApiClient,
            ProductoApiClient productoApiClient,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _pedidoId = pedidoId;
            _esAdmin = esAdmin;
            _pedidoApiClient = pedidoApiClient;
            _productoApiClient = productoApiClient;
            _serviceProvider = serviceProvider;

            _lineasDePedido = new BindingList<LineaPedidoDTO>();
            _productosDelProveedor = new List<ProductoDTO>();

            dataGridDetalle.AutoGenerateColumns = false;

            StyleManager.ApplyDataGridViewStyle(dataGridDetalle);
            StyleManager.ApplyButtonStyle(btnAgregarLinea);
            StyleManager.ApplyButtonStyle(btnEliminarLinea);
            StyleManager.ApplyButtonStyle(btnEditarCantidad);
            StyleManager.ApplyButtonStyle(btnConfirmarCambios);
            StyleManager.ApplyButtonStyle(btnCerrar);
        }

        private async void DetallePedidoForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                _pedido = await _pedidoApiClient.GetByIdAsync(_pedidoId);
                if (_pedido == null)
                {
                    MessageBox.Show("No se pudo cargar el detalle del pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                if (_pedido.IdProveedor <= 0)
                {
                    MessageBox.Show("El pedido no tiene un proveedor asignado válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                _productosDelProveedor = await _productoApiClient.GetProductosByProveedorIdAsync(_pedido.IdProveedor) ?? new List<ProductoDTO>();


                lblIdPedido.Text = $"ID Pedido: {_pedido.IdPedido}";
                lblProveedor.Text = $"Proveedor: {_pedido.ProveedorRazonSocial}";
                lblFecha.Text = $"Fecha: {_pedido.Fecha:dd/MM/yyyy}";

                _lineasDePedido = new BindingList<LineaPedidoDTO>(_pedido.LineasPedido ?? new List<LineaPedidoDTO>());

                ConfigurarColumnas();
                dataGridDetalle.DataSource = _lineasDePedido;

                ConfigurarVisibilidadControles();
                ActualizarTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el detalle del pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarVisibilidadControles()
        {
            if (_pedido == null) return;
            bool pedidoPendiente = "Pendiente".Equals(_pedido.Estado, StringComparison.OrdinalIgnoreCase);
            bool puedeEditar = _esAdmin && pedidoPendiente;

            btnAgregarLinea.Visible = puedeEditar;
            btnEliminarLinea.Visible = puedeEditar;
            btnEditarCantidad.Visible = puedeEditar;
            btnConfirmarCambios.Visible = puedeEditar;
            btnConfirmarCambios.Enabled = _datosModificados;
            dataGridDetalle.ReadOnly = !puedeEditar;

            if (dataGridDetalle.Columns.Contains("Cantidad"))
            {
                dataGridDetalle.Columns["Cantidad"].ReadOnly = !puedeEditar;
            }
        }

        private void ConfigurarColumnas()
        {
            if (dataGridDetalle.Columns.Count > 0 && dataGridDetalle.Columns.Contains("NombreProducto")) return;

            dataGridDetalle.Columns.Clear();
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreProducto", DataPropertyName = "NombreProducto", HeaderText = "Producto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", DataPropertyName = "Cantidad", HeaderText = "Cantidad", Width = 80 });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrecioUnitario", DataPropertyName = "PrecioUnitario", HeaderText = "Precio Unit.", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, ReadOnly = true });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", DataPropertyName = "Subtotal", HeaderText = "Subtotal", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, ReadOnly = true });
        }

        private void ActualizarTotal()
        {
            decimal totalCalculado = 0m;

            if (_lineasDePedido != null && _lineasDePedido.Count > 0)
            {
                totalCalculado = _lineasDePedido.Sum(l => l.Cantidad * l.PrecioUnitario);
            }

            if (_pedido != null)
            {
                _pedido.Total = totalCalculado;
            }

            var texto = $"Total: {totalCalculado.ToString("C2", System.Globalization.CultureInfo.CurrentCulture)}";
            lblTotal.Text = texto;

            if (TextRenderer.MeasureText(texto, lblTotal.Font).Width > lblTotal.Width)
            {
                lblTotal.Width = TextRenderer.MeasureText(texto, lblTotal.Font).Width + 20;
            }
        }


        private void MarcarComoModificado()
        {
            _datosModificados = true;
            btnConfirmarCambios.Enabled = true;
        }

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            if (_pedido == null || _pedido.IdProveedor <= 0) return;


            var idsProductosEnPedido = _lineasDePedido.Select(l => l.IdProducto).ToList();
            var productosDisponibles = _productosDelProveedor
                .Where(p => !idsProductosEnPedido.Contains(p.IdProducto))
                .ToList();

            if (!productosDisponibles.Any())
            {
                MessageBox.Show("No hay más productos de este proveedor para agregar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var form = _serviceProvider.GetRequiredService<AñadirProductoPedidoForm>();
            form.CargarProductosDisponibles(productosDisponibles);

            if (form.ShowDialog() == DialogResult.OK && form.ProductoSeleccionado != null)
            {
                decimal precioUnitario = form.ProductoSeleccionado.PrecioCompra;


                var nuevaLinea = new LineaPedidoDTO
                {
                    IdProducto = form.ProductoSeleccionado.IdProducto,
                    NombreProducto = form.ProductoSeleccionado.Nombre,
                    Cantidad = form.CantidadSeleccionada,
                    PrecioUnitario = precioUnitario
                };

                _lineasDePedido.Add(nuevaLinea);
                MarcarComoModificado();
                ActualizarTotal();
            }
        }

        private async void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridDetalle.CurrentRow?.DataBoundItem is LineaPedidoDTO linea)
            {
                var confirmResult = MessageBox.Show($"¿Está seguro de eliminar el producto '{linea.NombreProducto}' del pedido?",
                                                 "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    _lineasDePedido.Remove(linea);
                    ActualizarTotal(); 

                    if (_lineasDePedido.Count == 0)
                    {
                        await EliminarPedidoCompleto();
                    }
                    else
                    {
                        MarcarComoModificado(); 
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una línea para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task EliminarPedidoCompleto()
        {
            if (_pedido == null) return;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var response = await _pedidoApiClient.DeleteAsync(_pedido.IdPedido);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Pedido eliminado automáticamente al quedar vacío.", "Pedido Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _datosModificados = false; 
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al eliminar el pedido vacío: {error}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el pedido: {ex.Message}", "Error");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void btnEditarCantidad_Click(object sender, EventArgs e)
        {
            if (dataGridDetalle.CurrentRow != null && dataGridDetalle.Columns.Contains("Cantidad"))
            {
                dataGridDetalle.CurrentCell = dataGridDetalle.CurrentRow.Cells["Cantidad"];
                dataGridDetalle.BeginEdit(true);
            }
            else
            {
                MessageBox.Show("Seleccione una línea para editar la cantidad.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnConfirmarCambios_Click(object sender, EventArgs e)
        {
            if (_lineasDePedido.Count == 0)
            {
                var confirmDeletePedido = MessageBox.Show("El pedido está vacío. ¿Desea eliminar el pedido completo en lugar de guardar?",
                                                         "Pedido Vacío", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmDeletePedido == DialogResult.Yes)
                {
                    await EliminarPedidoCompleto();
                }
                return;
            }


            if (_pedido == null || !_datosModificados) return;
            var confirm = MessageBox.Show("¿Desea guardar los cambios en el pedido?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                _pedido.LineasPedido = _lineasDePedido.ToList();


                var response = await _pedidoApiClient.UpdateAsync(_pedido.IdPedido, _pedido);


                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cambios guardados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show($"Error al guardar los cambios: {ex.Message}", "Error");
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
                var result = MessageBox.Show(
                    "Hay cambios sin guardar. ¿Desea salir de todas formas?",
                    "Atención",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }


        private void dataGridDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dataGridDetalle.Columns["Cantidad"].Index) return;


            var lineaEditada = _lineasDePedido[e.RowIndex];
            var cellValue = dataGridDetalle.Rows[e.RowIndex].Cells["Cantidad"].Value;


            if (cellValue == null || !int.TryParse(cellValue.ToString(), out int nuevaCantidad) || nuevaCantidad <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad numérica válida y mayor a cero.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);

                dataGridDetalle.CancelEdit();

                return;
            }

            if (lineaEditada.Cantidad != nuevaCantidad)
            {
                lineaEditada.Cantidad = nuevaCantidad;
                _lineasDePedido.ResetItem(e.RowIndex);
                ActualizarTotal();
                MarcarComoModificado();
            }
        }


        private void dataGridDetalle_SelectionChanged(object sender, EventArgs e)
        {
            bool hayFilaSeleccionada = dataGridDetalle.CurrentRow != null;
            bool puedeEditar = _esAdmin && _pedido != null && "Pendiente".Equals(_pedido.Estado, StringComparison.OrdinalIgnoreCase);

            btnEliminarLinea.Enabled = hayFilaSeleccionada && puedeEditar;
            btnEditarCantidad.Enabled = hayFilaSeleccionada && puedeEditar;
        }
        private void dataGridDetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show($"Error en la grilla: {e.Exception.Message}\nContexto: {e.Context}", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ThrowException = false;
            e.Cancel = true;
        }

    }
}

