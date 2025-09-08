using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public MainForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void btnMenuProductos_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<ProductoForm>();
            form.ShowDialog();
        }

        private void btnMenuProvincias_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<Provincia>();
            form.ShowDialog();
        }

        private void btnMenuTiposProducto_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<TipoProducto>();
            form.ShowDialog();
        }
    }
}
