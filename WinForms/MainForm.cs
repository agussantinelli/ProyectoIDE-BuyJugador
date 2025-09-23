using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private string? _rolUsuario;

        public MainForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CenterTitle();
        }

        public void EstablecerRol(string? rol)
        {
            _rolUsuario = rol;
            ConfigurarVisibilidadBotones();
        }

        private void CenterTitle()
        {
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
        }

        // Controla qué botones son visibles según el rol del usuario.
        private void ConfigurarVisibilidadBotones()
        {
            // Ocultamos los botones de gestión por defecto.
            btnMenuPersonas.Visible = false;
            btnTipoProducto.Visible = false;
            btnProvincia.Visible = false; 

            // Mostramos los botones correspondientes al rol.
            if (_rolUsuario == "Dueño")
            {
                lblTitle.Text = "Panel del Administrador";
                btnMenuPersonas.Visible = true;
                btnTipoProducto.Visible = true;
                btnProvincia.Visible = true;
            }
            else if (_rolUsuario == "Empleado")
            {
                lblTitle.Text = "Panel de Empleado";
            }

            CenterTitle(); 
        }

        private void AbrirFormulario<T>() where T : Form
        {
            using var scope = _serviceProvider.CreateScope();
            var form = scope.ServiceProvider.GetRequiredService<T>();
            form.ShowDialog(this);
        }

        private void btnMenuProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<ProductoForm>();
        }

        private void btnMenuProvincias_Click(object sender, EventArgs e)
        {
            AbrirFormulario<ProvinciaForm>();
        }

        private void btnMenuTiposProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario<TipoProductoForm>();
        }

        private void btnMenuPersonas_Click(object sender, EventArgs e)
        {
            AbrirFormulario<PersonaForm>();
        }
    }
}

