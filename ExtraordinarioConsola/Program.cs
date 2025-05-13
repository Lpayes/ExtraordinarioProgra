using System;
using GimnasioManager.UI;
using GimnasioManager.Utils;

class Program
{
    static void Main()
    {
        var dbManager = new DatabaseManager();

        if (!dbManager.TestConnection())
        {
            Console.WriteLine("No se pudo conectar a la base de datos. La aplicación se cerrará.");
            return;
        }

        var menuManager = new MenuManager1();
        menuManager.MostrarMenuPrincipal();
    }
}
