using System;
using System.Windows.Forms;
using ApiClient;

namespace WinForms
{
    // MainForm es la ventana de entrada de la aplicación.
    // Su única responsabilidad es dirigir al usuario a otras ventanas.
    public partial class MainForm : Form
    {
        // Se define la URL base de la API.
        // Se usará para crear instancias de HttpClient en los clientes API.
        private const string BaseUrl = "https://localhost:7153/";

        public MainForm()
        {
            InitializeComponent();
            // Configura el título de la ventana.
            this.Text = "Menú Principal";

            // Se crean los botones para navegar a los formularios.
            var btnProvincias = new Button
            {
                Text = "Provincias",
                Location = new System.Drawing.Point(50, 50),
                Size = new System.Drawing.Size(150, 40)
            };
            btnProvincias.Click += BtnProvincias_Click;

            var btnTiposProducto = new Button
            {
                Text = "Tipos de Producto",
                Location = new System.Drawing.Point(50, 110),
                Size = new System.Drawing.Size(150, 40)
            };
            btnTiposProducto.Click += BtnTiposProducto_Click;

            // Agrega los botones al formulario.
            this.Controls.Add(btnProvincias);
            this.Controls.Add(btnTiposProducto);
        }

        // Manejador de eventos para el botón "Provincias".
        private void BtnProvincias_Click(object sender, EventArgs e)
        {
            // Crea una nueva instancia del formulario de provincias y la muestra.
            var httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            var provinciasForm = new ProvinciasForm(new ProvinciaApiClient(httpClient));
            provinciasForm.Show();
        }

        // Manejador de eventos para el botón "Tipos de Producto".
        private void BtnTiposProducto_Click(object sender, EventArgs e)
        {
            // Crea una nueva instancia del formulario de tipos de producto y la muestra.
            var httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            var tiposProductoForm = new TiposProductoForm(new TipoProductoApiClient(httpClient));
            tiposProductoForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
