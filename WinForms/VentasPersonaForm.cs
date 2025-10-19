using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class VentasPersonaForm : BaseForm
    {
        private readonly VentaApiClient _ventaApi;
        private readonly PersonaDTO _persona;
        private readonly IServiceProvider _serviceProvider;

        private readonly BindingSource _bs = new();
        private List<VentaDTO> _ventas = new();

        public VentasPersonaForm(
            VentaApiClient ventaApi,
            PersonaDTO persona,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _ventaApi = ventaApi;
            _persona = persona;
            _serviceProvider = serviceProvider;

            StyleManager.ApplyDataGridViewStyle(dgvVentas);
            StyleManager.ApplyButtonStyle(btnVerDetalle);
            StyleManager.ApplyButtonStyle(btnCerrar);

            lblTitulo.Text = $"Ventas de {_persona.NombreCompleto} (DNI {_persona.Dni})";
            cmbEstado.SelectedIndex = 0;

            dgvVentas.AutoGenerateColumns = false;
            ConfigurarColumnas();
            dgvVentas.DataSource = _bs;
        }

        private async void VentasPersonaForm_Load(object? sender, EventArgs e)
        {
            await CargarVentasAsync();
        }

        private async Task CargarVentasAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _ventas = await _ventaApi.GetByPersonaAsync(_persona.IdPersona) ?? new List<VentaDTO>();
                _ventas = _ventas.OrderByDescending(v => v.Fecha).ToList();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ConfigurarColumnas()
        {
            dgvVentas.Columns.Clear();
            dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IdVenta",
                HeaderText = "ID",
                Width = 70,
                FillWeight = 10
            });
            dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" },
                Width = 160,
                FillWeight = 20
            });
            dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                Width = 120,
                FillWeight = 15
            });
            dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Total",
                HeaderText = "Total",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = 120,
                FillWeight = 15
            });

            if (!dgvVentas.Columns.Contains("IdPersona"))
            {
                var col = new DataGridViewTextBoxColumn { DataPropertyName = "IdPersona", Visible = false };
                dgvVentas.Columns.Add(col);
            }
            if (!dgvVentas.Columns.Contains("NombreVendedor"))
            {
                var col = new DataGridViewTextBoxColumn { DataPropertyName = "NombreVendedor", Visible = false };
                dgvVentas.Columns.Add(col);
            }
            if (!dgvVentas.Columns.Contains("Lineas"))
            {
                var col = new DataGridViewTextBoxColumn { DataPropertyName = "Lineas", Visible = false };
                dgvVentas.Columns.Add(col);
            }
        }

        private void FiltrosChanged(object? sender, EventArgs e) => AplicarFiltros();

        private void AplicarFiltros()
        {
            var texto = (txtBuscar.Text ?? string.Empty).Trim();
            var estadoIdx = cmbEstado.SelectedIndex;

            var query = _ventas.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                query = query.Where(v =>
                    v.IdVenta.ToString().Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                    v.Fecha.ToString("dd/MM/yyyy HH:mm").Contains(texto, StringComparison.OrdinalIgnoreCase));
            }

            if (estadoIdx == 1) query = query.Where(v => string.Equals(v.Estado, "Pendiente", StringComparison.OrdinalIgnoreCase));
            if (estadoIdx == 2) query = query.Where(v => string.Equals(v.Estado, "Finalizada", StringComparison.OrdinalIgnoreCase));

            var lista = query.ToList();
            _bs.DataSource = lista;
            ActualizarEstadoBotones();
        }

        private void DgvVentas_SelectionChanged(object? sender, EventArgs e) => ActualizarEstadoBotones();

        private void ActualizarEstadoBotones()
        {
            btnVerDetalle.Enabled = dgvVentas.CurrentRow != null;
        }

        private void DgvVentas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) AbrirDetalleSeleccion();
        }

        private void BtnVerDetalle_Click(object? sender, EventArgs e) => AbrirDetalleSeleccion();

        private void AbrirDetalleSeleccion()
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaDTO v) return;

            var existing = this.MdiParent?.MdiChildren
                .OfType<DetalleVentaForm>()
                .FirstOrDefault(f => f.Tag is int id && id == v.IdVenta);

            if (existing != null)
            {
                existing.BringToFront();
                return;
            }

            var detalle = new DetalleVentaForm(
                v.IdVenta,
                esAdmin: true, 
                _serviceProvider.GetRequiredService<VentaApiClient>(),
                _serviceProvider.GetRequiredService<ProductoApiClient>(),
                _serviceProvider
            );

            detalle.Tag = v.IdVenta;
            detalle.MdiParent = this.MdiParent;

            detalle.FormClosed += async (s, args) =>
            {
                if (detalle.DialogResult == DialogResult.OK)
                    await CargarVentasAsync();
            };

            detalle.Show();
        }

        private void BtnCerrar_Click(object? sender, EventArgs e) => Close();
    }
}
