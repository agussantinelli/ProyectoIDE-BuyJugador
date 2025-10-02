using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class TipoProductoForm : BaseForm
    {
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private List<TipoProductoDTO> _tiposCache = new();
        private string _filtroActual = string.Empty;


        public TipoProductoForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _tipoProductoApiClient = serviceProvider.GetRequiredService<TipoProductoApiClient>();
            this.StartPosition = FormStartPosition.CenterParent;

            // Aplicamos el estilo
            StyleManager.ApplyDataGridViewStyle(dgvTiposProducto);
            StyleManager.ApplyButtonStyle(btnNuevo);
            StyleManager.ApplyButtonStyle(btnEditar);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnVolver);
        }

        public TipoProductoForm()
        {
            InitializeComponent();
        }



        private async void TipoProductoForm_Load(object sender, EventArgs e)
        {
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            dgvTiposProducto.ClearSelection();
            await CargarTipos();
            AplicarFiltro();
        }

        private async Task CargarTipos()
        {
            try
            {
                var tipos = await _tipoProductoApiClient.GetAllAsync() ?? new List<TipoProductoDTO>();
                _tiposCache = tipos.ToList();
                dgvTiposProducto.DataSource = _tiposCache.ToList();
                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvTiposProducto.Columns.Contains("IdTipoProducto"))
            {
                dgvTiposProducto.Columns["IdTipoProducto"].HeaderText = "Código";
                dgvTiposProducto.Columns["IdTipoProducto"].Width = 80;
            }
            if (dgvTiposProducto.Columns.Contains("Descripcion"))
            {
                dgvTiposProducto.Columns["Descripcion"].HeaderText = "Descripción";
                dgvTiposProducto.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var f = _filtroActual.ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(f))
                dgvTiposProducto.DataSource = _tiposCache.ToList();
            else
                dgvTiposProducto.DataSource = _tiposCache
                    .Where(t => t.Descripcion != null && t.Descripcion.ToLower().Contains(f))
                    .ToList();

            ConfigurarColumnas();
        }

        private TipoProductoDTO? ObtenerSeleccionado()
        {
            var row = dgvTiposProducto.SelectedRows.Count > 0
                ? dgvTiposProducto.SelectedRows[0]
                : dgvTiposProducto.CurrentRow;

            return row?.DataBoundItem as TipoProductoDTO;
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = new CrearTipoProductoForm(_tipoProductoApiClient);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarTipos();
                AplicarFiltro();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            var tipo = ObtenerSeleccionado();
            if (tipo == null) return;

            using var form = new EditarTipoProductoForm(_tipoProductoApiClient, tipo);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarTipos();
                AplicarFiltro();
            }
        }


        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var tipo = ObtenerSeleccionado();
            if (tipo == null) return;

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea eliminar el tipo \"{tipo.Descripcion}\"?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                await _tipoProductoApiClient.DeleteAsync(tipo.IdTipoProducto);
                _tiposCache.RemoveAll(t => t.IdTipoProducto == tipo.IdTipoProducto);
                AplicarFiltro();

                MessageBox.Show("Tipo de producto eliminado exitosamente.",
                    "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error de red: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar tipo de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void dgvTiposProducto_SelectionChanged(object sender, EventArgs e)
        {
            bool seleccionado = dgvTiposProducto.SelectedRows.Count > 0;
            btnEditar.Visible = seleccionado;
            btnEliminar.Visible = seleccionado;
        }
    }
}

