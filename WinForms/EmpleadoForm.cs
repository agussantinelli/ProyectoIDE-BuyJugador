using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace WinForms
{
    public partial class EmpleadoForm : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public EmpleadoForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void btnMenuProductos_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<ProductoForm>();
            form.ShowDialog();
        }

        private void btnMenuPersonas_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<PersonaForm>();
            form.ShowDialog();
        }
    }
}
