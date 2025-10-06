using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public partial class MainForm : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSessionService _userSessionService;
        private Panel? menuPanel;

        // No declaramos un nuevo MenuStrip aquí, usamos el 'menuStrip1' del Designer.
        private ToolStripMenuItem verMenuItem;
        private ToolStripMenuItem provinciasMenuItem;
        private ToolStripMenuItem localidadesMenuItem;
        private ToolStripLabel usuarioLabel;

        public MainForm(IServiceProvider serviceProvider, UserSessionService userSessionService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _userSessionService = userSessionService;

            this.IsMdiContainer = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

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
            // Volvemos a tu estructura original que funcionaba bien.
            InitializeMenuBar();
            InitializeMainMenu();
            CenterMenuPanel();
        }

        private void InitializeMenuBar()
        {
            // Usamos 'menuStrip1', que viene del archivo Designer, en lugar de crear uno nuevo.
            menuStrip1.BackColor = Color.FromArgb(26, 32, 40);
            menuStrip1.ForeColor = Color.White;
            menuStrip1.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            menuStrip1.Renderer = new DarkMenuRenderer();

            // --- Menú "Ver" ---
            verMenuItem = new ToolStripMenuItem("Ver");
            provinciasMenuItem = new ToolStripMenuItem("Provincias");
            localidadesMenuItem = new ToolStripMenuItem("Localidades");
            provinciasMenuItem.Click += (s, e) => AbrirFormulario<ProvinciaForm>();
            localidadesMenuItem.Click += (s, e) => AbrirFormulario<LocalidadForm>();
            verMenuItem.DropDownItems.AddRange(new ToolStripItem[] { provinciasMenuItem, localidadesMenuItem });

            // --- Botón "Cerrar Sesión" ---
            var btnCerrarSesion = new ToolStripMenuItem("Cerrar Sesión")
            {
                Alignment = ToolStripItemAlignment.Right // Alineado a la derecha
            };
            btnCerrarSesion.Click += btnCerrarSesion_Click; // Asignamos el evento

            // --- Etiqueta de Usuario ---
            usuarioLabel = new ToolStripLabel($"Usuario: {_userSessionService.CurrentUser?.NombreCompleto ?? "N/A"}")
            {
                Alignment = ToolStripItemAlignment.Right,
                ForeColor = Color.White,
                Font = new Font("Century Gothic", 10F, FontStyle.Regular),
                Padding = new Padding(0, 0, 20, 0)
            };

            // Limpiamos items por si acaso y añadimos los nuestros en el orden correcto
            menuStrip1.Items.Clear();
            menuStrip1.Items.Add(verMenuItem);
            menuStrip1.Items.Add(btnCerrarSesion);
            menuStrip1.Items.Add(usuarioLabel);
        }

        // Este método se mantiene exactamente como lo tenías, porque funcionaba bien.
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
                Text = _userSessionService.EsAdmin ? "Panel de Administrador" : "Panel de Empleado",
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

            if (_userSessionService.EsAdmin)
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

        private void CenterMenuPanel()
        {
            if (menuPanel != null)
            {
                int x = (this.ClientSize.Width - menuPanel.Width) / 2;
                // Corregimos el cálculo de 'y' para que use 'menuStrip1'
                int y = ((this.ClientSize.Height - menuStrip1.Height) - menuPanel.Height) / 2 + menuStrip1.Height;
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
            grid.Controls.Add(CreateMenuButton("btnPedidos", "Pedidos"), 1, 0);
            grid.Controls.Add(CreateMenuButton("btnPersonas", "Personas"), 0, 1);
            grid.Controls.Add(CreateMenuButton("btnTiposProducto", "Tipos de Productos"), 1, 1);
            grid.Controls.Add(CreateMenuButton("btnProductos", "Productos"), 0, 2);
            grid.Controls.Add(CreateMenuButton("btnProveedores", "Proveedores"), 1, 2);
        }

        private void CreateEmpleadoButtons(TableLayoutPanel grid)
        {
            grid.Controls.Add(CreateMenuButton("btnVentas", "Ventas"), 0, 0);
            grid.Controls.Add(CreateMenuButton("btnPedidos", "Pedidos"), 0, 1);
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
                    case "btnVentas": AbrirFormulario<VentaForm>(); break;
                    case "btnPedidos": AbrirFormulario<PedidoForm>(); break;
                    case "btnProductos": AbrirFormulario<ProductoForm>(); break;
                    case "btnPersonas": AbrirFormulario<PersonaForm>(); break;
                    case "btnTiposProducto": AbrirFormulario<TipoProductoForm>(); break;
                    case "btnProveedores": AbrirFormulario<ProveedorForm>(); break;
                    default: MessageBox.Show($"Funcionalidad no definida."); break;
                }
            }
        }

        private void AbrirFormulario<T>() where T : Form
        {
            if (menuPanel != null)
            {
                menuPanel.Visible = false;
            }

            var form = _serviceProvider.GetRequiredService<T>();
            form.MdiParent = this;
            form.FormClosed += (s, args) =>
            {
                if (this.MdiChildren.Length <= 1 && menuPanel != null)
                {
                    menuPanel.Visible = true;
                }
            };
            form.Show();
        }

        // Lógica de Cerrar Sesión (la que ya funcionaba bien)
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            _userSessionService.Logout();
            this.Close();
        }
    }

    public class DarkMenuRenderer : ToolStripProfessionalRenderer
    {
        public DarkMenuRenderer() : base(new DarkMenuColors()) { }

        private class DarkMenuColors : ProfessionalColorTable
        {
            public override Color MenuBorder => Color.FromArgb(40, 40, 40);
            public override Color MenuItemBorder => Color.FromArgb(0, 80, 200);
            public override Color MenuItemSelected => Color.FromArgb(0, 80, 200);
            public override Color ToolStripDropDownBackground => Color.FromArgb(45, 55, 70);
            public override Color ImageMarginGradientBegin => Color.FromArgb(45, 55, 70);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(45, 55, 70);
            public override Color ImageMarginGradientEnd => Color.FromArgb(45, 55, 70);
        }
    }
}