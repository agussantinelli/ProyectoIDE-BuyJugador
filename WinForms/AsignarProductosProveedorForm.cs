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
        private readonly string _razonSocial;
        private readonly ProductoApiClient _productoApiClient;
        private readonly ProductoProveedorApiClient _productoProveedorApiClient;
        private readonly PrecioCompraApiClient _precioCompraApiClient;

        private class ProductoDisponibleRow
        {
            public int IdProducto { get; set; }
            public string Nombre { get; set; }
        }

        private class ProductoAsignadoRow
        {
            public int IdProducto { get; set; }
            public string Nombre { get; set; }
            public decimal PrecioCompra { get; set; }
        }

        private BindingList<ProductoDisponibleRow> _disponiblesBindingList = new();
        private BindingList<ProductoAsignadoRow> _asignadosBindingList = new();
        private HashSet<int> _initialAssignedIds;

        public AsignarProductosProveedorForm(
            int idProveedor,
            string razonSocial,
            ProductoApiClient productoApiClient,
            ProductoProveedorApiClient productoProveedorApiClient,
            PrecioCompraApiClient precioCompraApiClient)
        {
            InitializeComponent();
            _idProveedor = idProveedor;
            _razonSocial = razonSocial;
            _productoApiClient = productoApiClient;
            _productoProveedorApiClient = productoProveedorApiClient;
            _precioCompraApiClient = precioCompraApiClient;

            this.Text = $"Asignar Productos a {_razonSocial}";

            StyleManager.ApplyDataGridViewStyle(dgvDisponibles);
            StyleManager.ApplyDataGridViewStyle(dgvAsignados);
            StyleManager.ApplyButtonStyle(btnAsignar);
            StyleManager.ApplyButtonStyle(btnQuitar);
            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void AsignarProductosProveedorForm_Load(object sender, EventArgs e)
        {
            try
            {
                var todosLosProductosTask = _productoApiClient.GetAllAsync();
                var productosAsignadosTask = _productoProveedorApiClient.GetProductosAsignadosByProveedorIdAsync(_idProveedor);
                await Task.WhenAll(todosLosProductosTask, productosAsignadosTask);

                var todosLosProductos = todosLosProductosTask.Result;
                var productosAsignadosConPrecio = productosAsignadosTask.Result;
                _initialAssignedIds = new HashSet<int>(productosAsignadosConPrecio.Select(p => p.IdProducto));

                var productosDisponibles = todosLosProductos
                    .Where(p => !_initialAssignedIds.Contains(p.IdProducto))
                    .Select(p => new ProductoDisponibleRow
                    {
                        IdProducto = p.IdProducto,
                        Nombre = p.Nombre
                    })
                    .ToList();

                var productosAsignados = productosAsignadosConPrecio
                    .Select(p => new ProductoAsignadoRow
                    {
                        IdProducto = p.IdProducto,
                        Nombre = p.Nombre,
                        PrecioCompra = p.PrecioCompra
                    })
                    .ToList();

                _disponiblesBindingList = new BindingList<ProductoDisponibleRow>(productosDisponibles);
                _asignadosBindingList = new BindingList<ProductoAsignadoRow>(productosAsignados);

                dgvDisponibles.DataSource = _disponiblesBindingList;
                dgvAsignados.DataSource = _asignadosBindingList;

                ConfigurarColumnasDisponibles();
                ConfigurarColumnasAsignados();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasDisponibles()
        {
            dgvDisponibles.AutoGenerateColumns = false;
            dgvDisponibles.Columns.Clear();

            dgvDisponibles.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ProductoDisponibleRow.IdProducto),
                    HeaderText = "ID",
                    Width = 60,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Font = new Font("Century Gothic", 9F, FontStyle.Bold)
                    }
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ProductoDisponibleRow.Nombre),
                    HeaderText = "Nombre",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 260
                }
            });
        }

        private void ConfigurarColumnasAsignados()
        {
            dgvAsignados.AutoGenerateColumns = false;
            dgvAsignados.Columns.Clear();

            dgvAsignados.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ProductoAsignadoRow.IdProducto),
                    HeaderText = "ID",
                    Width = 60,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Font = new Font("Century Gothic", 9F, FontStyle.Bold)
                    }
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ProductoAsignadoRow.Nombre),
                    HeaderText = "Nombre",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 210
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ProductoAsignadoRow.PrecioCompra),
                    HeaderText = "Precio Compra",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 100,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "C2",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                }
            });
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (dgvDisponibles.CurrentRow?.DataBoundItem is ProductoDisponibleRow productoSeleccionado)
            {
                using var formPrecio = new AsignarPrecioProductoProveedorForm(
                    _precioCompraApiClient,
                    _productoProveedorApiClient,
                    productoSeleccionado.IdProducto,
                    _idProveedor,
                    productoSeleccionado.Nombre,
                    _razonSocial);

                if (formPrecio.ShowDialog() == DialogResult.OK)
                {
                    _disponiblesBindingList.Remove(productoSeleccionado);
                    _asignadosBindingList.Add(new ProductoAsignadoRow
                    {
                        IdProducto = productoSeleccionado.IdProducto,
                        Nombre = productoSeleccionado.Nombre,
                        PrecioCompra = formPrecio.Precio
                    });
                }
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvAsignados.CurrentRow?.DataBoundItem is ProductoAsignadoRow productoSeleccionado)
            {
                _asignadosBindingList.Remove(productoSeleccionado);
                _disponiblesBindingList.Add(new ProductoDisponibleRow
                {
                    IdProducto = productoSeleccionado.IdProducto,
                    Nombre = productoSeleccionado.Nombre
                });
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var finalAssignedIds = new HashSet<int>(_asignadosBindingList.Select(p => p.IdProducto));
                var idsToRemove = _initialAssignedIds.Except(finalAssignedIds).ToList();
                foreach (var idProducto in idsToRemove)
                    await _productoProveedorApiClient.DeleteAsync(idProducto, _idProveedor);

                var idsToAdd = finalAssignedIds.Except(_initialAssignedIds).ToList();
                foreach (var idProducto in idsToAdd)
                {
                    var productoAsignado = _asignadosBindingList.First(p => p.IdProducto == idProducto);
                    var ppDto = new ProductoProveedorDTO { IdProducto = idProducto, IdProveedor = _idProveedor };
                    await _productoProveedorApiClient.CreateAsync(ppDto);
                    var pcDto = new PrecioCompraDTO
                    {
                        IdProducto = idProducto,
                        IdProveedor = _idProveedor,
                        Monto = productoAsignado.PrecioCompra
                    };
                    await _precioCompraApiClient.CreateAsync(pcDto);
                }

                MessageBox.Show("Cambios guardados exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los cambios: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
