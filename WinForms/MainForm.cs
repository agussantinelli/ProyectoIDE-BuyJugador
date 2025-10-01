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
        private Panel? menuPanel;

        public MainForm(IServiceProvider serviceProvider, bool isAdmin)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _isAdmin = isAdmin;

            // Establece el color de fondo del área MDI para que coincida con el menú
            foreach (Control control in this.Controls)
            {
                if (control is MdiClient client)
                {
                    client.BackColor = Color.FromArgb(49, 66, 82);
                    break;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeMainMenu();
            CenterMenuPanel();
        }

        /// <summary>
        /// Crea y configura el panel del menú principal con sus botones.
        /// </summary>
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
                RowCount = 3
            };
            menuPanel.Controls.Add(buttonGrid);
            buttonGrid.BringToFront();

            this.Controls.Add(menuPanel);
            menuPanel.BringToFront();

            // Configura la grilla de botones según el rol del usuario
            if (_isAdmin)
            {
                buttonGrid.ColumnCount = 2;
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

        /// <summary>
        /// Centra el panel del menú en el formulario principal.
        /// </summary>
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

        private void CreateAdminButtons(TableLayoutPanel grid)
        {
            grid.Controls.Add(CreateMenuButton("btnVentas", "Ventas"), 0, 0);
            grid.Controls.Add(CreateMenuButton("btnNuevoPedido", "Nuevo Pedido"), 1, 0);
            grid.Controls.Add(CreateMenuButton("btnEmpleados", "Empleados"), 0, 1);
            grid.Controls.Add(CreateMenuButton("btnTiposProducto", "Tipos de Productos"), 1, 1);
            grid.Controls.Add(CreateMenuButton("btnProductos", "Productos"), 0, 2);
            grid.Controls.Add(CreateMenuButton("btnProveedores", "Proveedores"), 1, 2);
        }

        private void CreateEmpleadoButtons(TableLayoutPanel grid)
        {
            grid.Controls.Add(CreateMenuButton("btnVentas", "Ventas"), 0, 0);
            grid.Controls.Add(CreateMenuButton("btnNuevoPedido", "Nuevo Pedido"), 0, 1);
            grid.Controls.Add(CreateMenuButton("btnProductos", "Productos"), 0, 2);
        }

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

        private void MenuButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                switch (clickedButton.Name)
                {
                    case "btnVentas":
                        AbrirFormulario(_serviceProvider.GetRequiredService<VentaForm>());
                        break;
                    case "btnNuevoPedido":
                        MessageBox.Show("El formulario de pedidos aún no ha sido implementado.", "En desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "btnProductos":
                        AbrirFormulario(_serviceProvider.GetRequiredService<ProductoForm>());
                        break;
                    case "btnEmpleados":
                        AbrirFormulario(_serviceProvider.GetRequiredService<PersonaForm>());
                        break;
                    case "btnTiposProducto":
                        AbrirFormulario(_serviceProvider.GetRequiredService<TipoProductoForm>());
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

        /// <summary>
        /// Abre un formulario como hijo MDI, ocultando el menú principal.
        /// </summary>
        private void AbrirFormulario(Form form)
        {
            if (menuPanel != null)
            {
                menuPanel.Visible = false;
            }

            form.MdiParent = this;
            form.FormClosed += (s, args) => {
                // Si no quedan otros formularios hijos abiertos, vuelve a mostrar el menú.
                if (this.MdiChildren.Length <= 1 && menuPanel != null)
                {
                    menuPanel.Visible = true;
                }
            };
            form.Show();
        }
    }
}

