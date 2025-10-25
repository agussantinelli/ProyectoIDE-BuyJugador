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

        private DateTime _today = DateTime.Today;
        private readonly DateTime _hardMin = new DateTime(2022, 1, 1);
        private DateTime? _minFromByData;
        private DateTime EffectiveMinFrom =>
            (_minFromByData.HasValue && _minFromByData.Value > _hardMin)
                ? _minFromByData.Value.Date
                : _hardMin;

        private DateTime _from;
        private DateTime _to;

        public ReporteHistorialPreciosForm(PrecioVentaApiClient precioApiClient,
                                             ReporteApiClient reporteApiClient)
        {
            InitializeComponent();
            _precioApiClient = precioApiClient;
            _reporteApiClient = reporteApiClient;
        }

        private async void HistorialPreciosForm_Load(object sender, EventArgs e)
        {
            await InitRangeAsync();

            await CargarDatosDelGrafico();
        }

        private async Task InitRangeAsync()
        {
            try
            {
                var hist = await _precioApiClient.GetHistorialAsync();
                _minFromByData = hist?
                    .SelectMany(p => p.Puntos)
                    .Select(p => (DateTime?)p.Fecha.Date)
                    .OrderBy(d => d)
                    .FirstOrDefault();

                _from = _today.AddDays(-180);
                if (_from < EffectiveMinFrom) _from = EffectiveMinFrom;

                _to = _today;
                NormalizeCoherence();
            }
            catch
            {
                _from = _today.AddDays(-180);
                if (_from < _hardMin) _from = _hardMin;
                _to = _today;
                NormalizeCoherence();
            }
        }

        private void NormalizeFrom()
        {
            if (_from < EffectiveMinFrom) _from = EffectiveMinFrom;
            if (_from > _to) _to = _from;
        }

        private void NormalizeTo()
        {
            if (_to > _today) _to = _today;
            if (_to < _from) _from = _to;
        }

        private void NormalizeCoherence()
        {
            if (_from > _to) _from = _to;
        }

        private async void btnExportarPdf_Click(object sender, EventArgs e)
        {
            try
            {
                NormalizeFrom();
                NormalizeTo();
                NormalizeCoherence();

                var bytes = await _reporteApiClient.GetHistorialPreciosPdfAsync(_from, _to, 1200, 500);
                if (bytes is null)
                {
                    MessageBox.Show("No se pudo generar el PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = $"HistorialDePrecios_{DateTime.Now:dd-MM-yyyy_HH.mm.ss}.pdf"
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
            Cursor = Cursors.WaitCursor;

            formsPlot1.Plot.Title("Evolución de Precios de Venta");
            formsPlot1.Plot.XLabel("Fecha");
            formsPlot1.Plot.YLabel("Precio");
            formsPlot1.Plot.Axes.Bottom.TickGenerator = new DateTimeAutomatic();

            try
            {
                NormalizeFrom();
                NormalizeTo();
                NormalizeCoherence();

                var historialProductos = await _precioApiClient.GetHistorialAsync();
                if (historialProductos == null || !historialProductos.Any())
                {
                    MessageBox.Show("No se encontraron datos para generar el reporte.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                formsPlot1.Plot.Clear();

                foreach (var producto in historialProductos)
                {
                    var puntos = producto.Puntos
                        .Where(p => p.Fecha.Date >= _from && p.Fecha.Date <= _to)
                        .OrderBy(p => p.Fecha)
                        .ToList();

                    if (puntos.Count > 1)
                    {
                        double[] fechas = puntos.Select(p => p.Fecha.ToOADate()).ToArray();
                        double[] montos = puntos.Select(p => (double)p.Monto).ToArray();

                        var scatter = formsPlot1.Plot.Add.Scatter(fechas, montos);
                        scatter.Label = producto.NombreProducto;
                        scatter.LineWidth = 2;
                        scatter.MarkerSize = 2;
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
                Cursor = Cursors.Default;
            }
        }
    }
}

