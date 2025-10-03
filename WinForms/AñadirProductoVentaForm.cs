using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class AñadirProductoVentaForm : Form
    {
        // Propiedades públicas para que DetalleVentaForm pueda acceder a los datos seleccionados.
        public ProductoDTO ProductoSeleccionado { get; private set; }
        public int CantidadSeleccionada { get; private set; }

        public AñadirProductoVentaForm(List<ProductoDTO> productosDisponibles)
        {
            InitializeComponent();
            ConfigurarDataGridView();
            dgvProductos.DataSource = productosDisponibles;

            // Aplicar estilos a los controles
            StyleManager.ApplyDataGridViewStyle(dgvProductos);
            StyleManager.ApplyButtonStyle(btnAceptar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private void ConfigurarDataGridView()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Producto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioActual", HeaderText = "Precio", Width = 80 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Stock", HeaderText = "Stock", Width = 60 });
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un producto
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un producto de la lista.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que la cantidad sea un número válido y mayor a cero
            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad numérica válida y mayor a cero.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asignar los valores a las propiedades públicas
            ProductoSeleccionado = (ProductoDTO)dgvProductos.SelectedRows[0].DataBoundItem;
            CantidadSeleccionada = cantidad;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
