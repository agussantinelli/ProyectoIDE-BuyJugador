using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class ReporteVentasForm : BaseForm
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly ReporteApiClient _reporteApiClient;

        public ReporteVentasForm(PersonaApiClient personaApiClient, ReporteApiClient reporteApiClient)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            _reporteApiClient = reporteApiClient;

            StyleManager.ApplyDataGridViewStyle(dgvReporte);
            ConfigurarColumnas(); // #NUEVO: Llamada a la configuración manual de columnas.
        }

        // #NUEVO: Método para configurar manualmente las columnas del DataGridView.
        // #Intención: Controlar el ancho, alineación y formato para una mejor visualización.
        private void ConfigurarColumnas()
        {
            dgvReporte.AutoGenerateColumns = false;
            dgvReporte.Columns.Clear();

            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IdVenta",
                HeaderText = "ID Venta",
                Width = 100
            });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, // La columna se expande para llenar el espacio.
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                Width = 150
            });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalVenta",
                HeaderText = "Total",
                Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Formato de moneda.
                    Alignment = DataGridViewContentAlignment.MiddleRight // Alineación a la derecha.
                }
            });
        }

        private async void ReporteVentasForm_Load(object sender, EventArgs e)
        {
            this.Text = "Reporte de Ventas por Vendedor (Últimos 7 días)";
            lblInfo.Text = "Seleccione un vendedor para generar el reporte.";
            lblTotal.Text = string.Empty;

            try
            {
                var personas = await _personaApiClient.GetPersonasActivasParaReporteAsync();
                if (personas != null)
                {
                    cmbVendedores.DataSource = personas;
                    cmbVendedores.DisplayMember = "NombreCompleto";
                    cmbVendedores.ValueMember = "IdPersona";
                    cmbVendedores.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vendedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cmbVendedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVendedores.SelectedValue is int idPersona)
            {
                try
                {
                    lblInfo.Text = "Generando reporte...";
                    lblTotal.Text = string.Empty;
                    dgvReporte.DataSource = null;

                    var reporteData = await _reporteApiClient.GetReporteVentasPorVendedorAsync(idPersona);

                    if (reporteData == null || !reporteData.Any())
                    {
                        lblInfo.Text = "No se encontraron ventas para este vendedor en los últimos 7 días.";
                    }
                    else
                    {
                        dgvReporte.DataSource = reporteData;
                        // #NUEVO: Calcular y mostrar el total en la etiqueta.
                        var totalGeneral = reporteData.Sum(item => item.TotalVenta);
                        lblTotal.Text = $"Total General: {totalGeneral:C2}";
                        lblInfo.Text = $"Mostrando {reporteData.Count} ventas.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblInfo.Text = "Error al generar el reporte.";
                }
            }
        }
    }
}

