using System;
using System.Windows.Forms;

namespace WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnProvincias_Click(object sender, EventArgs e)
        {
            // Crea una nueva instancia de ProvinciasForm y la muestra
            ProvinciasForm provinciasForm = new ProvinciasForm();
            provinciasForm.Show();
        }

        private void btnTiposProducto_Click(object sender, EventArgs e)
        {
            // Crea una nueva instancia de TiposProductoForm y la muestra
            TiposProductoForm tiposProductoForm = new TiposProductoForm();
            tiposProductoForm.Show();
        }
    }
}