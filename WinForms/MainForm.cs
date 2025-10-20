using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class MainForm : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSessionService _userSessionService;
        private Panel? menuPanel;
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

            this.MdiChildActivate += MainForm_MdiChildActivate;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeMenuBar();
            InitializeMainMenu();
            CenterMenuPanel();
            if (menuPanel != null)
            {
                menuPanel.Visible = true;
            }
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {

            this.BeginInvoke(new Action(() =>
            {
                if (menuPanel != null)
                {
                    bool showPanel = this.MdiChildren.Length == 0;
                    menuPanel.Visible = showPanel;

                    if (showPanel)
                    {
                        menuPanel.BringToFront();
                    }
                }
            }));
        }

        private void InitializeMenuBar()
        {
            menuStrip1.BackColor = Color.FromArgb(26, 32, 40);
            menuStrip1.ForeColor = Color.White;
            menuStrip1.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            menuStrip1.Renderer = new DarkMenuRenderer();

            menuStrip1.Items.Clear();

            var verMenuItem = new ToolStripMenuItem("Ver");
            var provinciasMenuItem = new ToolStripMenuItem("Provincias");
            var localidadesMenuItem = new ToolStripMenuItem("Localidades");
            provinciasMenuItem.Click += (s, e) => AbrirFormulario<ProvinciaForm>();
            localidadesMenuItem.Click += (s, e) => AbrirFormulario<LocalidadForm>();
            verMenuItem.DropDownItems.AddRange(new ToolStripItem[] { provinciasMenuItem, localidadesMenuItem });

            var btnCerrarSesion = new ToolStripMenuItem("Cerrar Sesión")
            {
                Alignment = ToolStripItemAlignment.Right
            };
            btnCerrarSesion.Click += btnCerrarSesion_Click;

            usuarioLabel = new ToolStripLabel($"Usuario: {_userSessionService.CurrentUser?.NombreCompleto ?? "N/A"}")
            {
                Alignment = ToolStripItemAlignment.Right,
                ForeColor = Color.White,
                Font = new Font("Century Gothic", 10F, FontStyle.Regular),
                Padding = new Padding(0, 0, 20, 0)
            };

            menuStrip1.Items.Add(verMenuItem);
            menuStrip1.Items.Add(btnCerrarSesion);
            menuStrip1.Items.Add(usuarioLabel);
        }

        private void InitializeMainMenu()
        {
            menuPanel = new Panel
            {
                Size = new Size(800, 600),
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
                Padding = new Padding(20)
            };
            menuPanel.Controls.Add(buttonGrid);
            buttonGrid.BringToFront();

            this.Controls.Add(menuPanel);
            menuPanel.BringToFront();

            if (_userSessionService.EsAdmin)
            {
                buttonGrid.ColumnCount = 2;
                buttonGrid.RowCount = 4;
                buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

                CreateAdminButtons(buttonGrid);
            }
            else
            {
                buttonGrid.ColumnCount = 2;
                buttonGrid.RowCount = 2;
                buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                CreateEmpleadoButtons(buttonGrid);
            }
        }

        private void CenterMenuPanel()
        {
            if (menuPanel != null)
            {
                int x = (this.ClientSize.Width - menuPanel.Width) / 2;
                int y = ((this.ClientSize.Height - menuStrip1.Height) - menuPanel.Height) / 2 + menuStrip1.Height;
                menuPanel.Location = new Point(Math.Max(x, 0), Math.Max(y, menuStrip1.Height));
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

            var reportButtonsPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0) 
            };
            reportButtonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            reportButtonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            var btnReporteVentas = CreateMenuButton("btnReporteVentas", "Reporte de Ventas");
            var btnReportePrecios = CreateMenuButton("btnReportePrecios", "Reporte Hist. Precios");

            btnReporteVentas.Margin = new Padding(15, 30, 15, 30);
            btnReportePrecios.Margin = new Padding(15, 30, 15, 30);
            btnReporteVentas.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnReportePrecios.Font = new Font("Century Gothic", 10F, FontStyle.Bold);

            reportButtonsPanel.Controls.Add(btnReporteVentas, 0, 0);
            reportButtonsPanel.Controls.Add(btnReportePrecios, 1, 0);

            grid.Controls.Add(reportButtonsPanel, 0, 3);
            grid.SetColumnSpan(reportButtonsPanel, 2);
        }

        private void CreateEmpleadoButtons(TableLayoutPanel grid)
        {
            grid.Controls.Add(CreateMenuButton("btnVentas", "Ventas"), 0, 0);
            grid.Controls.Add(CreateMenuButton("btnProveedores", "Proveedores"), 1, 0);
            grid.Controls.Add(CreateMenuButton("btnPedidos", "Pedidos"), 0, 1);
            grid.Controls.Add(CreateMenuButton("btnProductos", "Productos"), 1, 1);
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
                    // Botones de acción
                    case "btnVentas": AbrirFormulario<VentaForm>(); break;
                    case "btnPedidos": AbrirFormulario<PedidoForm>(); break;
                    case "btnProductos": AbrirFormulario<ProductoForm>(); break;
                    case "btnPersonas": AbrirFormulario<PersonaForm>(); break;
                    case "btnTiposProducto": AbrirFormulario<TipoProductoForm>(); break;
                    case "btnProveedores": AbrirFormulario<ProveedorForm>(); break;

                    // Botones de reportes
                    case "btnReporteVentas": AbrirFormulario<ReporteVentasForm>(); break;
                    case "btnReportePrecios": AbrirFormulario<ReporteHistorialPreciosForm>(); break;

                    default: MessageBox.Show($"Funcionalidad no definida."); break;
                }
            }
        }

        private void AbrirFormulario<T>() where T : Form
        {
            // Busca si el formulario ya está abierto para evitar duplicados.
            var form = this.MdiChildren.OfType<T>().FirstOrDefault();

            if (form != null)
            {
                // Si existe, lo trae al frente.
                form.BringToFront();
            }
            else
            {
                // Si no existe, lo crea a través del IServiceProvider y lo muestra.
                form = _serviceProvider.GetRequiredService<T>();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            _userSessionService.Logout();
            this.DialogResult = DialogResult.OK; 
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
