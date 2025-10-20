using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class ReporteVentasForm : BaseForm
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly ReporteApiClient _reporteApiClient;
        private List<ReporteVentasDTO> _currentReportData;

        public ReporteVentasForm(PersonaApiClient personaApiClient, ReporteApiClient reporteApiClient)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            _reporteApiClient = reporteApiClient;

            StyleManager.ApplyDataGridViewStyle(dgvReporte);
            StyleManager.ApplyButtonStyle(btnDescargarPdf);
        }

        private async void ReporteVentasForm_Load(object sender, EventArgs e)
        {
            this.Text = "Reporte de Ventas por Vendedor (Últimos 7 días)";
            lblInfo.Text = "Seleccione un vendedor para generar el reporte.";
            btnDescargarPdf.Enabled = false;
            lblTotal.Text = "";

            try
            {
                var personas = await _personaApiClient.GetPersonasActivasParaReporteAsync();
                if (personas != null)
                {
                    personas.Insert(0, new PersonaSimpleDTO { IdPersona = 0, NombreCompleto = "-- Seleccione un Vendedor --" });

                    cmbVendedores.DataSource = personas;
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
                    btnDescargarPdf.Enabled = false;
                    lblTotal.Text = "";
                    _currentReportData = await _reporteApiClient.GetReporteVentasPorVendedorAsync(idPersona);
                    dgvReporte.DataSource = _currentReportData;

                    ConfigurarColumnas();

                    if (_currentReportData == null || !_currentReportData.Any())
                    {
                        lblInfo.Text = "No se encontraron ventas para este vendedor en los últimos 7 días.";
                    }
                    else
                    {
                        lblInfo.Text = $"Mostrando {_currentReportData.Count} ventas.";
                        btnDescargarPdf.Enabled = true;
                        decimal totalGeneral = _currentReportData.Sum(item => item.TotalVenta);
                        lblTotal.Text = $"Total General: {totalGeneral:C2}";
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
                _currentReportData = null;
                lblInfo.Text = "Seleccione un vendedor para generar el reporte.";
                lblTotal.Text = "";
                btnDescargarPdf.Enabled = false;
            }
        }

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
                HeaderText = "Fecha y Hora",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                Width = 120
            });

            var totalColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalVenta",
                HeaderText = "Total",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            };
            dgvReporte.Columns.Add(totalColumn);
        }

        private async void btnDescargarPdf_Click(object sender, EventArgs e)
        {
            if (cmbVendedores.SelectedValue is not int idPersona || idPersona == 0 || _currentReportData == null || !_currentReportData.Any())
            {
                MessageBox.Show("No hay datos para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Document (*.pdf)|*.pdf";
                var selectedPersona = cmbVendedores.SelectedItem as PersonaSimpleDTO;

                var nombreVendedor = selectedPersona?.NombreCompleto?.Replace(" ", "_") ?? "Usuario";
                saveFileDialog.FileName = $"{nombreVendedor} Reporte {DateTime.Now:dd-MM-yyyy}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        btnDescargarPdf.Text = "Generando...";
                        btnDescargarPdf.Enabled = false;

                        var pdfBytes = await _reporteApiClient.GetReporteVentasPdfAsync(idPersona);
                        if (pdfBytes != null)
                        {
                            await File.WriteAllBytesAsync(saveFileDialog.FileName, pdfBytes);
                            MessageBox.Show($"Reporte guardado exitosamente en:\n{saveFileDialog.FileName}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo generar el archivo PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error al guardar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        btnDescargarPdf.Text = "Descargar PDF";
                        btnDescargarPdf.Enabled = true;
                    }
                }
            }
        }
    }
}

