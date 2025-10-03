using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class AsignarProductosProveedorForm : BaseForm
    {
        private readonly int _idProveedor;
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
            _productoApiClient = productoApiClient;
            _productoProveedorApiClient = productoProveedorApiClient;

            this.Text = $"Asignar Productos a {razonSocial}";
            this.StartPosition = FormStartPosition.CenterScreen;

            StyleManager.ApplyDataGridViewStyle(dgvDisponibles);
            StyleManager.ApplyDataGridViewStyle(dgvAsignados);
            StyleManager.ApplyButtonStyle(btnAsignar);
            StyleManager.ApplyButtonStyle(btnQuitar);
            StyleManager.ApplyButtonStyle(btnGuardar);
        }

        private async void AsignarProductosProveedorForm_Load(object sender, EventArgs e)
        {
            await CargarProductos();
        }

        private async Task CargarProductos()
        {
            try
            {
                var todosLosProductos = await _productoApiClient.GetAllAsync() ?? new List<ProductoDTO>();
                var productosAsignadosIds = (await _productoProveedorApiClient.GetByProveedorIdAsync(_idProveedor))
                                            .Select(pp => pp.IdProducto)
                                            .ToHashSet();

                var asignados = todosLosProductos.Where(p => productosAsignadosIds.Contains(p.IdProducto)).ToList();
                var disponibles = todosLosProductos.Where(p => !productosAsignadosIds.Contains(p.IdProducto)).ToList();

                _asignadosBindingList = new BindingList<ProductoDTO>(asignados);
                _disponiblesBindingList = new BindingList<ProductoDTO>(disponibles);

                dgvAsignados.DataSource = _asignadosBindingList;
                dgvDisponibles.DataSource = _disponiblesBindingList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (dgvDisponibles.CurrentRow?.DataBoundItem is ProductoDTO producto)
            {
                _disponiblesBindingList.Remove(producto);
                _asignadosBindingList.Add(producto);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvAsignados.CurrentRow?.DataBoundItem is ProductoDTO producto)
            {
                _asignadosBindingList.Remove(producto);
                _disponiblesBindingList.Add(producto);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var dto = new ProductoProveedorDTO
            {
                IdProveedor = _idProveedor,
                IdsProducto = _asignadosBindingList.Select(p => p.IdProducto).ToList()
            };

            try
            {
                await _productoProveedorApiClient.UpdateProductosProveedorAsync(dto);
                MessageBox.Show("Asignación de productos guardada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

