using System;
using System.Windows.Forms;

namespace WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Se cambió el formulario de inicio de Inventario a MainForm.
            Application.Run(new MainForm());
        }
    }
}
