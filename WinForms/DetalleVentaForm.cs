using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class DetalleVentaForm : BaseForm
    {
        public VentaDTO Venta { get; set; }
        public List<LineaVentaDTO> Lineas { get; set; }
        public bool EsAdmin { get; set; }

        private readonly LineaVentaApiClient _lineaVentaApiClient;
        private readonly ProductoApiClient _productoApiClient;
        private List<ProductoDTO> _todosLosProductos;
        private bool _datosModificados = false;

        public DetalleVentaForm(LineaVentaApiClient lineaVentaApiClient, ProductoApiClient productoApiClient)
        {
            InitializeComponent();
            _lineaVentaApiClient = lineaVentaApiClient;
            _productoApiClient = productoApiClient;
            _todosLosProductos = new List<ProductoDTO>();

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
            if (Venta == null || Lineas == null)
            {
                MessageBox.Show("No se proporcionaron los datos de la venta para mostrar el detalle.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                _todosLosProductos = await _productoApiClient.GetAllAsync() ?? new List<ProductoDTO>();

                lblIdVenta.Text = $"ID Venta: {Venta.IdVenta}";
                lblFecha.Text = $"Fecha: {Venta.Fecha:dd/MM/yyyy HH:mm}";
                lblVendedor.Text = $"Vendedor: {Venta.NombreVendedor}";

                btnEliminarLinea.Visible = EsAdmin;
                dataGridDetalle.ReadOnly = !EsAdmin;

                ConfigurarColumnasDetalle();
                RefrescarGrid();

                dataGridDetalle.ClearSelection();
                btnEditarCantidad.Enabled = false;
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

        private void ConfigurarColumnasDetalle()
        {
            dataGridDetalle.AutoGenerateColumns = false;
            dataGridDetalle.Columns.Clear();

            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreProducto", HeaderText = "Producto", DataPropertyName = "NombreProducto", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cantidad", DataPropertyName = "Cantidad", ReadOnly = !EsAdmin });
            dataGridDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal", DataPropertyName = "Subtotal", ReadOnly = true, DefaultCellStyle = { Format = "C2" } });
        }

        private void RefrescarGrid()
        {
            dataGridDetalle.DataSource = null;
            if (Lineas != null)
            {
                dataGridDetalle.DataSource = Lineas;
                lblTotal.Text = $"Total: {Lineas.Sum(l => l.Subtotal):C2}";
            }
        }

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            if (!EsAdmin)
            {
                MessageBox.Show("Solo los administradores pueden agregar productos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new SeleccionarProductoForm(_todosLosProductos);
            if (form.ShowDialog() == DialogResult.OK && form.ProductoSeleccionado != null)
            {
                var producto = form.ProductoSeleccionado;

                var lineaExistente = Lineas.FirstOrDefault(l => l.IdProducto == producto.IdProducto);

                if (lineaExistente != null)
                {
                    lineaExistente.Cantidad += form.CantidadSeleccionada;
                    lineaExistente.Subtotal = lineaExistente.Cantidad * producto.PrecioActual;
                }
                else
                {
                    var nuevaLinea = new LineaVentaDTO
                    {
                        IdVenta = Venta.IdVenta,
                        IdProducto = producto.IdProducto,
                        NombreProducto = producto.Nombre,
                        Cantidad = form.CantidadSeleccionada,
                        Subtotal = form.CantidadSeleccionada * producto.PrecioActual,
                        NroLineaVenta = Lineas.Any() ? Lineas.Max(l => l.NroLineaVenta) + 1 : 1,
                        EsNueva = true
                    };
                    Lineas.Add(nuevaLinea);
                }

                MarcarComoModificado();
                RefrescarGrid();
            }
        }

        private async void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridDetalle.CurrentRow?.DataBoundItem is not LineaVentaDTO selectedLinea)
            {
                MessageBox.Show("Seleccione una línea para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"¿Desea eliminar el producto '{selectedLinea.NombreProducto}' de la venta?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                var response = await _lineaVentaApiClient.DeleteAsync(selectedLinea.IdVenta, selectedLinea.NroLineaVenta);
                if (response.IsSuccessStatusCode)
                {
                    var producto = _todosLosProductos.FirstOrDefault(p => p.IdProducto == selectedLinea.IdProducto);
                    if (producto != null)
                    {
                        producto.Stock += selectedLinea.Cantidad;
                    }

                    Lineas.Remove(selectedLinea);
                    MarcarComoModificado();
                    RefrescarGrid();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la línea.", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (!_datosModificados)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            var confirm = MessageBox.Show("Tienes cambios sin guardar. ¿Estás seguro de que quieres salir sin guardar?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }


        private void btnEditarCantidad_Click(object sender, EventArgs e)
        {
            if (dataGridDetalle.CurrentRow == null || !EsAdmin)
            {
                MessageBox.Show("Seleccione una línea para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int rowIndex = dataGridDetalle.CurrentRow.Index;
            var colIndex = dataGridDetalle.Columns["Cantidad"].Index;

            dataGridDetalle.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridDetalle.ClearSelection();

            dataGridDetalle.CurrentCell = dataGridDetalle.Rows[rowIndex].Cells[colIndex];

            dataGridDetalle.BeginEdit(true);
        }

        private async void dataGridDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !EsAdmin) return;

            var lineaEditada = Lineas[e.RowIndex];
            if (!int.TryParse(dataGridDetalle.Rows[e.RowIndex].Cells["Cantidad"].Value?.ToString(), out int nuevaCantidad) || nuevaCantidad <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad válida.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridDetalle.CancelEdit();
                RefrescarGrid();
                return;
            }

            var producto = _todosLosProductos.FirstOrDefault(p => p.IdProducto == lineaEditada.IdProducto);
            if (producto == null)
            {
                MessageBox.Show("No se encontró el producto para validar el stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridDetalle.CancelEdit();
                RefrescarGrid();
                return;
            }

            int cantidadOriginal = lineaEditada.Cantidad;
            int diferencia = nuevaCantidad - cantidadOriginal;

            if (producto.Stock < diferencia)
            {
                MessageBox.Show($"Stock insuficiente. Stock disponible para añadir: {producto.Stock}", "Error de Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridDetalle.CancelEdit();
                RefrescarGrid();
                return;
            }

            lineaEditada.Cantidad = nuevaCantidad;
            lineaEditada.Subtotal = nuevaCantidad * (producto.PrecioActual);

            MarcarComoModificado(); 

            RefrescarGrid();
        }


        private void MarcarComoModificado()
        {
            _datosModificados = true;
            btnConfirmarCambios.Enabled = true;
        }

        private async void btnConfirmarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                foreach (var linea in Lineas)
                {
                    HttpResponseMessage response;

                    if (linea.EsNueva)
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
            btnEditarCantidad.Enabled = dataGridDetalle.CurrentRow != null && EsAdmin;
        }
    }
}

