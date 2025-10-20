using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Data;
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
        }

        private async void ReporteVentasForm_Load(object sender, EventArgs e)
        {
            this.Text = "Reporte de Ventas por Vendedor (Últimos 7 días)";
            lblInfo.Text = "Seleccione un vendedor para generar el reporte.";

            try
            {
                var personas = await _personaApiClient.GetAllAsync();
                if (personas != null)
                {
                    var vendedores = personas.Where(p => p.FechaIngreso.HasValue).OrderBy(p => p.NombreCompleto).ToList();
                    vendedores.Insert(0, new PersonaDTO { IdPersona = 0, NombreCompleto = "-- Por favor, elija un vendedor --" });

                    cmbVendedores.DataSource = vendedores;
                    cmbVendedores.DisplayMember = "NombreCompleto";
                    cmbVendedores.ValueMember = "IdPersona";
                    cmbVendedores.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vendedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cmbVendedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVendedores.SelectedValue is int idPersona && idPersona > 0)
            {
                try
                {
                    lblInfo.Text = "Generando reporte...";
                    dgvReporte.DataSource = null;
                    var reporteData = await _reporteApiClient.GetReporteVentasPorVendedorAsync(idPersona);
                    dgvReporte.DataSource = reporteData;

                    ConfigurarColumnas();

                    if (reporteData == null || !reporteData.Any())
                    {
                        lblInfo.Text = "No se encontraron ventas para este vendedor en los últimos 7 días.";
                    }
                    else
                    {
                        var total = reporteData.Sum(r => r.TotalVenta);
                        lblInfo.Text = $"Mostrando {reporteData.Count} ventas. Total: {total:C2}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblInfo.Text = "Error al generar el reporte.";
                }
            }
            else
            {
                dgvReporte.DataSource = null;
                lblInfo.Text = "Seleccione un vendedor para generar el reporte.";
            }
        }

        private void ConfigurarColumnas()
        {
            dgvReporte.AutoGenerateColumns = false;
            dgvReporte.Columns.Clear();

            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdVenta", HeaderText = "ID Venta", Width = 100 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Fecha", HeaderText = "Fecha", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado", HeaderText = "Estado", Width = 120 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalVenta", HeaderText = "Total", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight } });
        }
    }
}
