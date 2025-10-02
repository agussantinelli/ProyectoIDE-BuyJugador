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

            // Aplicar estilos
            StyleManager.ApplyDataGridViewStyle(dataGridDetalle);
            StyleManager.ApplyButtonStyle(btnEliminarLinea);
            StyleManager.ApplyButtonStyle(btnEditarCantidad);
            StyleManager.ApplyButtonStyle(btnCerrar); // Estandarizado
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

            try
            {
                var response = await _lineaVentaApiClient.UpdateAsync(lineaEditada.IdVenta, lineaEditada.NroLineaVenta, new LineaVentaDTO { Cantidad = nuevaCantidad });
                if (response.IsSuccessStatusCode)
                {
                    producto.Stock -= diferencia;
                    lineaEditada.Cantidad = nuevaCantidad;
                    lineaEditada.Subtotal = nuevaCantidad * (producto.PrecioActual);
                    _datosModificados = true;
                    RefrescarGrid();
                }
                else
                {
                    MessageBox.Show("Error al actualizar la línea.", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RefrescarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RefrescarGrid();
            }

            dataGridDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                    _datosModificados = true;
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
            this.DialogResult = _datosModificados ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
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

        private void dataGridDetalle_SelectionChanged(object sender, EventArgs e)
        {
            btnEditarCantidad.Enabled = dataGridDetalle.CurrentRow != null && EsAdmin;
        }
    }
}

