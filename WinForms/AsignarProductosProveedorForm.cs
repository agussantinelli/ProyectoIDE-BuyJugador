using DTOs;
using ApiClient;
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
        private readonly ProductoApiClient _productoApiClient;
        private readonly ProductoProveedorApiClient _productoProveedorApiClient;

        private BindingList<ProductoDTO> _asignados = new();
        private BindingList<ProductoDTO> _noAsignados = new();

        public AsignarProductosProveedorForm(int idProveedor, string nombreProveedor, ProductoApiClient productoApiClient, ProductoProveedorApiClient productoProveedorApiClient)
        {
            InitializeComponent();
            _idProveedor = idProveedor;
            _productoApiClient = productoApiClient;
            _productoProveedorApiClient = productoProveedorApiClient;

            this.Text = $"Asignar Productos a: {nombreProveedor}";
            StyleManager.ApplyPrimaryButtonStyle(btnGuardar);
            StyleManager.ApplySecondaryButtonStyle(btnCancelar);
        }

        private async void AsignarProductosProveedorForm_Load(object sender, EventArgs e)
        {
            try
            {
                var todosProductosTask = _productoApiClient.GetProductosAsync();
                var asignadosTask = _productoApiClient.GetProductosByProveedorIdAsync(_idProveedor);
                await Task.WhenAll(todosProductosTask, asignadosTask);

                var todos = (await todosProductosTask) ?? new List<ProductoDTO>();
                var asignados = (await asignadosTask) ?? new List<ProductoDTO>();

                _asignados = new BindingList<ProductoDTO>(asignados);
                _noAsignados = new BindingList<ProductoDTO>(todos.Where(p => !asignados.Any(a => a.IdProducto == p.IdProducto)).ToList());

                lstAsignados.DataSource = _asignados;
                lstAsignados.DisplayMember = "Nombre";
                lstNoAsignados.DataSource = _noAsignados;
                lstNoAsignados.DisplayMember = "Nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error");
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (lstNoAsignados.SelectedItem is ProductoDTO seleccionado)
            {
                _noAsignados.Remove(seleccionado);
                _asignados.Add(seleccionado);
            }
        }

        private void btnDesasignar_Click(object sender, EventArgs e)
        {
            if (lstAsignados.SelectedItem is ProductoDTO seleccionado)
            {
                _asignados.Remove(seleccionado);
                _noAsignados.Add(seleccionado);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var dto = new ProductoProveedorDTO
            {
                IdProveedor = _idProveedor,
                IdsProducto = _asignados.Select(p => p.IdProducto).ToList()
            };

            try
            {
                await _productoProveedorApiClient.UpdateProductosProveedorAsync(dto);
                MessageBox.Show("Asignación de productos guardada.", "Éxito");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();
    }
}

