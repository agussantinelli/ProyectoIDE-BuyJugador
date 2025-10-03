using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class DetalleVentaForm : BaseForm
    {
        public VentaDTO Venta { get; set; }
        private BindingList<LineaVentaDTO> _lineasDeVenta;
        public bool EsAdmin { get; set; }

        private readonly LineaVentaApiClient _lineaVentaApiClient;
        private readonly ProductoApiClient _productoApiClient;
        private List<ProductoDTO> _todosLosProductos;
        private bool _datosModificados = false;
        private int _cantidadOriginalEdit;

        public DetalleVentaForm(LineaVentaApiClient lineaVentaApiClient, ProductoApiClient productoApiClient)
        {
            InitializeComponent();
            _lineaVentaApiClient = lineaVentaApiClient;
            _productoApiClient = productoApiClient;
            _todosLosProductos = new List<ProductoDTO>();

            this.StartPosition = FormStartPosition.CenterScreen;

            StyleManager.ApplyDataGridViewStyle(dataGridDetalle);
            StyleManager.ApplyButtonStyle(btnEliminarLinea);
            StyleManager.ApplyButtonStyle(btnCerrar);
            StyleManager.ApplyButtonStyle(btnAgregarLinea);
            StyleManager.ApplyButtonStyle(btnConfirmarCambios);

            btnEditarCantidad.Visible = false;
        }

        private async void DetalleVentaForm_Load(object sender, EventArgs e)
        {
            if (Venta != null)
            {
                lblIdVenta.Text = $"ID Venta: {Venta.IdVenta}";
                lblFecha.Text = $"Fecha: {Venta.Fecha.ToShortDateString()}";
                lblVendedor.Text = $"Vendedor: {Venta.NombreVendedor}";

                await CargarProductos();
                CargarLineasDeVenta();
                ActualizarTotal();
                ConfigurarVisibilidadControles();
            }
        }

        private async Task CargarProductos()
        {
            try
            {
                _todosLosProductos = await _productoApiClient.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarLineasDeVenta()
        {
            _lineasDeVenta = new BindingList<LineaVentaDTO>(Venta.Lineas);
            dataGridDetalle.DataSource = _lineasDeVenta;

            if (dataGridDetalle.Columns["Cantidad"] != null)
            {
                dataGridDetalle.Columns["Cantidad"].ReadOnly = !EsAdmin;
            }

            if (dataGridDetalle.Columns["IdVenta"] != null)
            {
                dataGridDetalle.Columns["IdVenta"].Visible = false;
            }
        }

        private void ConfigurarVisibilidadControles()
        {
            btnEliminarLinea.Visible = EsAdmin;
            btnAgregarLinea.Visible = EsAdmin;
            btnConfirmarCambios.Visible = EsAdmin;
            btnConfirmarCambios.Enabled = false;
        }

        private void ActualizarTotal()
        {
            Venta.Total = _lineasDeVenta.Sum(l => l.Subtotal);
            lblTotal.Text = $"Total: ${Venta.Total:N2}";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            var idsProductosEnVenta = _lineasDeVenta.Select(lv => lv.IdProducto).ToList();
            var productosDisponibles = _todosLosProductos.Where(p => !idsProductosEnVenta.Contains(p.IdProducto) && p.Stock > 0).ToList();

            if (!productosDisponibles.Any())
            {
                MessageBox.Show("No hay más productos disponibles para agregar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var form = new AñadirProductoVentaForm(productosDisponibles))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var productoSeleccionado = form.ProductoSeleccionado;
                    var cantidadSeleccionada = form.CantidadSeleccionada;

                    if (productoSeleccionado.Stock < cantidadSeleccionada)
                    {
                        MessageBox.Show($"Stock insuficiente. Disponible: {productoSeleccionado.Stock}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var nuevaLinea = new LineaVentaDTO
                    {
                        IdVenta = Venta.IdVenta,
                        NroLineaVenta = 0,
                        IdProducto = productoSeleccionado.IdProducto,
                        NombreProducto = productoSeleccionado.Nombre,
                        Cantidad = cantidadSeleccionada,
                        PrecioUnitario = productoSeleccionado.PrecioActual,
                        Subtotal = cantidadSeleccionada * productoSeleccionado.PrecioActual
                    };

                    _lineasDeVenta.Add(nuevaLinea);
                    productoSeleccionado.Stock -= cantidadSeleccionada;

                    MarcarComoModificado();
                }
            }
        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridDetalle.CurrentRow != null)
            {
                var lineaAEliminar = (LineaVentaDTO)dataGridDetalle.CurrentRow.DataBoundItem;
                _lineasDeVenta.Remove(lineaAEliminar);

                var producto = _todosLosProductos.FirstOrDefault(p => p.IdProducto == lineaAEliminar.IdProducto);
                if (producto != null)
                {
                    producto.Stock += lineaAEliminar.Cantidad;
                }
                MarcarComoModificado();
            }
        }

        private void MarcarComoModificado()
        {
            _datosModificados = true;
            btnConfirmarCambios.Enabled = true;
            ActualizarTotal();
        }

        private async void btnConfirmarCambios_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var lineasActuales = _lineasDeVenta.ToList();

                foreach (var lineaOriginal in Venta.Lineas)
                {
                    if (!lineasActuales.Any(l => l.NroLineaVenta == lineaOriginal.NroLineaVenta && l.NroLineaVenta != 0))
                    {
                        await _lineaVentaApiClient.DeleteAsync(lineaOriginal.IdVenta, lineaOriginal.NroLineaVenta);
                    }
                }

                foreach (var linea in lineasActuales)
                {
                    HttpResponseMessage response;
                    if (linea.NroLineaVenta == 0)
                    {
                        response = await _lineaVentaApiClient.CreateAsync(linea);
                    }
                    else
                    {
                        response = await _lineaVentaApiClient.UpdateAsync(linea.IdVenta, linea.NroLineaVenta, linea);
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al guardar la línea {linea.NroLineaVenta}: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }

                MessageBox.Show("Cambios guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _datosModificados = false;
                btnConfirmarCambios.Enabled = false;

                this.DialogResult = DialogResult.OK;
                this.Close();
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

        private void dataGridDetalle_SelectionChanged(object sender, EventArgs e)
        {
            bool hayFilaSeleccionada = dataGridDetalle.CurrentRow != null;
            btnEliminarLinea.Enabled = hayFilaSeleccionada && EsAdmin;
        }

        private void dataGridDetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridDetalle.Columns[e.ColumnIndex].Name == "Cantidad" && EsAdmin)
            {
                _cantidadOriginalEdit = (int)dataGridDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        private void dataGridDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridDetalle.Columns[e.ColumnIndex].Name == "Cantidad" && EsAdmin)
            {
                var lineaEditada = (LineaVentaDTO)dataGridDetalle.Rows[e.RowIndex].DataBoundItem;

                if (!int.TryParse(dataGridDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out int nuevaCantidad) || nuevaCantidad <= 0)
                {
                    MessageBox.Show("La cantidad debe ser un número positivo.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lineaEditada.Cantidad = _cantidadOriginalEdit;
                }
                else
                {
                    var producto = _todosLosProductos.FirstOrDefault(p => p.IdProducto == lineaEditada.IdProducto);
                    if (producto == null) return;

                    int cambioEnCantidad = nuevaCantidad - _cantidadOriginalEdit;

                    if (cambioEnCantidad > producto.Stock)
                    {
                        MessageBox.Show($"Stock insuficiente. Solo puede agregar {producto.Stock} unidades más.", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lineaEditada.Cantidad = _cantidadOriginalEdit;
                    }
                    else
                    {
                        producto.Stock -= cambioEnCantidad;
                        lineaEditada.Cantidad = nuevaCantidad;
                        lineaEditada.Subtotal = lineaEditada.Cantidad * lineaEditada.PrecioUnitario;
                        MarcarComoModificado();
                    }
                }
                _lineasDeVenta.ResetItem(e.RowIndex);
            }
        }

        // Método vacío para solucionar el error del diseñador.
        private void btnEditarCantidad_Click(object sender, EventArgs e)
        {
            // Este método está vacío intencionalmente porque el botón está oculto.
        }
    }
}

