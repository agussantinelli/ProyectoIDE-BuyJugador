using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class AsignarProductosProveedorForm : BaseForm
    {
        private readonly int _idProveedor;
        private readonly string _razonSocial;
        private readonly ProductoApiClient _productoApiClient;
        private readonly ProductoProveedorApiClient _productoProveedorApiClient;

        private BindingList<ProductoDTO> _disponiblesBindingList = new();
        private BindingList<ProductoDTO> _asignadosBindingList = new();

        public AsignarProductosProveedorForm(
            int idProveedor,
            string razonSocial,
            ProductoApiClient productoApiClient,
            ProductoProveedorApiClient productoProveedorApiClient)
        {
            InitializeComponent();
            _idProveedor = idProveedor;
            _razonSocial = razonSocial;
            _productoApiClient = productoApiClient;
            _productoProveedorApiClient = productoProveedorApiClient;

            // Aplicar estilos a los controles
            StyleManager.ApplyDataGridViewStyle(dgvDisponibles);
            StyleManager.ApplyDataGridViewStyle(dgvAsignados);
            StyleManager.ApplyButtonStyle(btnAsignar);
            StyleManager.ApplyButtonStyle(btnQuitar);
            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void AsignarProductosProveedorForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Asignar Productos a {_razonSocial}";

            // Configurar las columnas de los DataGridViews
            ConfigurarDataGridView(dgvDisponibles);
            ConfigurarDataGridView(dgvAsignados);

            try
            {
                // Cargar todos los productos y los ya asignados
                var todosLosProductos = await _productoApiClient.GetAllAsync();
                var productosAsignados = await _productoProveedorApiClient.GetByProveedorIdAsync(_idProveedor);
                var idsAsignados = productosAsignados.Select(p => p.IdProducto).ToHashSet();

                var productosDisponibles = todosLosProductos.Where(p => !idsAsignados.Contains(p.IdProducto)).ToList();

                _disponiblesBindingList = new BindingList<ProductoDTO>(productosDisponibles);
                _asignadosBindingList = new BindingList<ProductoDTO>(productosAsignados);

                dgvDisponibles.DataSource = _disponiblesBindingList;
                dgvAsignados.DataSource = _asignadosBindingList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = true; // Permitir selección múltiple

            dgv.Columns.Clear();

            // Añadir columna de CheckBox para selección
            var checkColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Seleccionar",
                HeaderText = "",
                Width = 30
            };
            dgv.Columns.Add(checkColumn);

            // Columnas visibles
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IdProducto",
                HeaderText = "ID",
                Name = "IdProducto",
                ReadOnly = true,
                Width = 50
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Name = "Nombre",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción",
                Name = "Descripcion",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        // Mueve los productos seleccionados (con checkbox) de disponibles a asignados
        private void btnAsignar_Click(object sender, EventArgs e)
        {
            var seleccionados = new List<ProductoDTO>();
            foreach (DataGridViewRow row in dgvDisponibles.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Seleccionar"].Value))
                {
                    seleccionados.Add(row.DataBoundItem as ProductoDTO);
                }
            }

            foreach (var producto in seleccionados)
            {
                _disponiblesBindingList.Remove(producto);
                _asignadosBindingList.Add(producto);
            }
        }

        // Mueve los productos seleccionados (con checkbox) de asignados a disponibles
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            var seleccionados = new List<ProductoDTO>();
            foreach (DataGridViewRow row in dgvAsignados.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Seleccionar"].Value))
                {
                    seleccionados.Add(row.DataBoundItem as ProductoDTO);
                }
            }

            foreach (var producto in seleccionados)
            {
                _asignadosBindingList.Remove(producto);
                _disponiblesBindingList.Add(producto);
            }
        }


        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var idsProductoAsignados = _asignadosBindingList.Select(p => p.IdProducto).ToList();
            try
            {
                var response = await _productoProveedorApiClient.UpdateProductosProveedorAsync(_idProveedor, idsProductoAsignados);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Asignación de productos guardada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Error al guardar. Código: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

