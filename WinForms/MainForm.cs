using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly bool _isAdmin;
        private Panel menuPanel; 

        public MainForm(IServiceProvider serviceProvider, bool isAdmin)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _isAdmin = isAdmin;

            // Establece el color de fondo del área MDI
            foreach (Control control in this.Controls)
            {
                if (control is MdiClient)
                {
                    control.BackColor = Color.FromArgb(49, 66, 82);
                    break;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeMainMenu();
            CenterMenuPanel();
        }

        private void InitializeMainMenu()
        {
            menuPanel = new Panel
            {
                Size = new Size(800, 500),
                BackColor = Color.FromArgb(45, 55, 70),
                Anchor = AnchorStyles.None
            };

            Label titleLabel = new Label
            {
                Text = _isAdmin ? "Panel de Administrador" : "Panel de Empleados",
                ForeColor = Color.White,
                Font = new Font("Century Gothic", 18F, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 80
            };
            menuPanel.Controls.Add(titleLabel);

            TableLayoutPanel buttonGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                ColumnCount = 2,
                RowCount = 3
            };

            menuPanel.Controls.Add(buttonGrid);
            buttonGrid.BringToFront();

            this.Controls.Add(menuPanel);
            menuPanel.BringToFront();

            if (_isAdmin)
            {
                buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                CreateAdminButtons(buttonGrid);
            }
            else
            {
                buttonGrid.ColumnCount = 1;
                buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                CreateEmpleadoButtons(buttonGrid);
            }
        }

        // Centra el panel del menú en el formulario
        private void CenterMenuPanel()
        {
            if (menuPanel != null)
            {
                int x = (this.ClientSize.Width - menuPanel.Width) / 2;
                int y = (this.ClientSize.Height - menuPanel.Height) / 2;
                menuPanel.Location = new Point(x, y);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            CenterMenuPanel();
        }

        // Crea y añade los botones para el rol de Administrador
        private void CreateAdminButtons(TableLayoutPanel grid)
        {
            grid.Controls.Add(CreateMenuButton("btnNuevaVenta", "Nueva Venta"), 0, 0);
            grid.Controls.Add(CreateMenuButton("btnNuevoPedido", "Nuevo Pedido"), 1, 0);
            grid.Controls.Add(CreateMenuButton("btnEmpleados", "Empleados"), 0, 1);
            grid.Controls.Add(CreateMenuButton("btnTiposProducto", "Tipos de Productos"), 1, 1);
            grid.Controls.Add(CreateMenuButton("btnProductos", "Productos"), 0, 2);
            grid.Controls.Add(CreateMenuButton("btnProveedores", "Proveedores"), 1, 2);
        }

        // Crea y añade los botones para el rol de Empleado
        private void CreateEmpleadoButtons(TableLayoutPanel grid)
        {
            // Botones actualizados para el rol de Empleado
            grid.Controls.Add(CreateMenuButton("btnNuevaVenta", "Nueva Venta"), 0, 0);
            grid.Controls.Add(CreateMenuButton("btnNuevoPedido", "Nuevo Pedido"), 0, 1);
            grid.Controls.Add(CreateMenuButton("btnProductos", "Productos"), 0, 2);
        }

        // Método de ayuda para crear y dar estilo a cada botón del menú
        private Button CreateMenuButton(string name, string text)
        {
            Button btn = new Button
            {
                Name = name,
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(15),
                BackColor = Color.FromArgb(26, 32, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Century Gothic", 12F, FontStyle.Bold),
            };
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 80, 200);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 200);
            btn.Click += MenuButton_Click;
            return btn;
        }

        // Maneja el evento de clic para todos los botones del menú
        private void MenuButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                switch (clickedButton.Name)
                {
                    // Casos para Administrador y Empleado
                    case "btnNuevaVenta":
                        MessageBox.Show("El formulario de ventas aún no ha sido implementado.", "En desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "btnNuevoPedido":
                        MessageBox.Show("El formulario de pedidos aún no ha sido implementado.", "En desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "btnProductos":
                        AbrirFormulario(_serviceProvider.GetRequiredService<ProductoForm>());
                        break;

                    // Casos solo para Administrador
                    case "btnEmpleados":
                        AbrirFormulario(_serviceProvider.GetRequiredService<PersonaForm>());
                        break;
                    case "btnTiposProducto":
                        AbrirFormulario(_serviceProvider.GetRequiredService<TipoProductoForm>());
                        break;
                    case "btnConsultarStock":
                        AbrirFormulario(_serviceProvider.GetRequiredService<ProductoForm>());
                        break;
                    case "btnClientes":
                        AbrirFormulario(_serviceProvider.GetRequiredService<PersonaForm>());
                        break;
                    case "btnProveedores":
                        AbrirFormulario(_serviceProvider.GetRequiredService<ProveedorForm>());
                        break;
                    default:
                        MessageBox.Show($"Funcionalidad para '{clickedButton.Text}' no definida.");
                        break;
                }
            }
        }

        // Abre un formulario como hijo MDI y gestiona la visibilidad del menú
        private void AbrirFormulario(Form form)
        {
            menuPanel.Visible = false;

            form.MdiParent = this;
            form.FormClosed += (s, args) => {
                if (this.MdiChildren.Length <= 1)
                {
                    menuPanel.Visible = true;
                }
            };
            form.Show();
        }
    }
}

