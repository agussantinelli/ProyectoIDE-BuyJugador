using ApiClient;
using ScottPlot;
using ScottPlot.TickGenerators;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class ReporteHistorialPreciosForm : BaseForm
    {
        private readonly PrecioVentaApiClient _precioApiClient;
        private readonly ReporteApiClient _reporteApiClient;

        public ReporteHistorialPreciosForm(PrecioVentaApiClient precioApiClient,
                                           ReporteApiClient reporteApiClient)
        {
            InitializeComponent();
            _precioApiClient = precioApiClient;
            _reporteApiClient = reporteApiClient;
        }

        private async void HistorialPreciosForm_Load(object sender, EventArgs e)
        {
            await CargarDatosDelGrafico();
        }

        private async void btnExportarPdf_Click(object sender, EventArgs e)
        {
            try
            {
                var desde = DateTime.Today.AddMonths(-6);
                var hasta = DateTime.Today;

                var bytes = await _reporteApiClient.GetHistorialPreciosPdfAsync(desde, hasta, 1200, 500);
                if (bytes is null)
                {
                    MessageBox.Show("No se pudo generar el PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = $"HistorialPrecios_{DateTime.Now:yyyyMMdd}.pdf"
                };
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(sfd.FileName, bytes);
                    MessageBox.Show("PDF exportado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar PDF: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarDatosDelGrafico()
        {
            this.Cursor = Cursors.WaitCursor;
            formsPlot1.Plot.Title("Evolución de Precios de Venta");
            formsPlot1.Plot.XLabel("Fecha");
            formsPlot1.Plot.YLabel("Precio");
            formsPlot1.Plot.Axes.Bottom.TickGenerator = new DateTimeAutomatic();

            try
            {
                var historialProductos = await _precioApiClient.GetHistorialAsync();
                if (historialProductos == null || !historialProductos.Any())
                {
                    MessageBox.Show("No se encontraron datos para generar el reporte.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var producto in historialProductos)
                {
                    if (producto.Puntos.Count > 1)
                    {
                        double[] fechas = producto.Puntos.Select(p => p.Fecha.ToOADate()).ToArray();
                        double[] montos = producto.Puntos.Select(p => (double)p.Monto).ToArray();

                        var scatter = formsPlot1.Plot.Add.Scatter(fechas, montos);
                        scatter.LegendText = producto.NombreProducto;
                        scatter.LineWidth = 2;
                    }
                }

                formsPlot1.Plot.ShowLegend(Alignment.UpperLeft);
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el reporte: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
