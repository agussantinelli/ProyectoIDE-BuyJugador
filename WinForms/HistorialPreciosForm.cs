using ApiClient;
using DTOs;
using ScottPlot;
// # AÑADIR ESTE USING: Es necesario para acceder a los generadores de Ticks.
using ScottPlot.TickGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class HistorialPreciosForm : BaseForm
    {
        private readonly PrecioVentaApiClient _precioApiClient;

        public HistorialPreciosForm(PrecioVentaApiClient precioApiClient)
        {
            InitializeComponent();
            _precioApiClient = precioApiClient;
        }

        private async void HistorialPreciosForm_Load(object sender, EventArgs e)
        {
            await CargarDatosDelGrafico();
        }

        private async Task CargarDatosDelGrafico()
        {
            this.Cursor = Cursors.WaitCursor;
            formsPlot1.Plot.Title("Evolución de Precios de Venta");
            formsPlot1.Plot.XLabel("Fecha");
            formsPlot1.Plot.YLabel("Precio");

            // # CORRECCIÓN DEFINITIVA: La sintaxis correcta para ScottPlot 5 es asignar 
            // # una instancia del generador de ticks automáticos de fecha y hora.
            formsPlot1.Plot.Axes.Bottom.TickGenerator = new DateTimeAutomatic();

            try
            {
                var historialProductos = await _precioApiClient.GetHistorialAsync();

                if (historialProductos == null || !historialProductos.Any())
                {
                    MessageBox.Show("No se encontraron datos para generar el reporte.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                formsPlot1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}