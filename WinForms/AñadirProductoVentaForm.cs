using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class AñadirProductoVentaForm : BaseForm
    {
        public ProductoDTO ProductoSeleccionado { get; private set; }
        public int CantidadSeleccionada { get; private set; }

        public AñadirProductoVentaForm()
        {
            InitializeComponent();
            StyleManager.ApplyButtonStyle(btnAceptar);
            StyleManager.ApplyButtonStyle(btnCancelar);
            StyleManager.ApplyDataGridViewStyle(dgvProductos);
        }

        public void CargarProductosDisponibles(List<ProductoDTO> productos)
        {
            dgvProductos.DataSource = productos;
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Producto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                DataPropertyName = "Stock",
                HeaderText = "Stock"
            });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                DataPropertyName = "PrecioActual",
                HeaderText = "Precio",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow?.DataBoundItem is ProductoDTO selectedProduct && int.TryParse(txtCantidad.Text, out int cantidad))
            {
                if (cantidad <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a cero.", "Cantidad inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cantidad > selectedProduct.Stock)
                {
                    MessageBox.Show($"La cantidad solicitada ({cantidad}) supera el stock disponible ({selectedProduct.Stock}).", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProductoSeleccionado = selectedProduct;
                CantidadSeleccionada = cantidad;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto e ingrese una cantidad numérica válida.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

