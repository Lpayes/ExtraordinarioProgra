using System;
using System.Windows.Forms;
using GimnasioManager.UI;
using GimnasioManager.Utils; 

namespace GimnasioManager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var dbManager = new DatabaseManager();
            if (!dbManager.TestConnection())
            {
                MessageBox.Show("No se pudo conectar a la base de datos. La aplicación se cerrará.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm());
        }
    }
}