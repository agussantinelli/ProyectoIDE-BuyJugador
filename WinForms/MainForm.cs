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

            this.WindowState = FormWindowState.Maximized;

        }

        private void AbrirFormulario<T>() where T : Form
        {
            pnlContenido.Controls.Clear();
            var form = _serviceProvider.GetRequiredService<T>();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(form);
            form.Show();
        }

        private void btnProductos_Click(object sender, EventArgs e) => AbrirFormulario<ProductoForm>();
        private void btnPersonas_Click(object sender, EventArgs e) => AbrirFormulario<PersonaForm>();
        private void btnTipoProducto_Click(object sender, EventArgs e) => AbrirFormulario<TipoProductoForm>();
        private void btnProvincias_Click(object sender, EventArgs e) => AbrirFormulario<ProvinciaForm>();
        private void btnLocalidades_Click(object sender, EventArgs e) => AbrirFormulario<LocalidadForm>();

        private void btnSalir_Click(object sender, EventArgs e) => Close();


    }
}
