using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();

            // Configuración para formularios hijos MDI
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ControlBox = true;
            this.MinimizeBox = true;
            this.MaximizeBox = true;
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.WindowsDefaultLocation;

            // Estilo base
            this.Font = new Font("Century Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.BackColor = Color.FromArgb(49, 66, 82);
            this.ForeColor = Color.White;
        }
    }
}

