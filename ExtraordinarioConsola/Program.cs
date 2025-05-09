using System;
using GimnasioManager.Models;
using GimnasioManager.Services;
using GimnasioManager.Utils;
using GimnasioManager.UI;

namespace GimnasioManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Sistema de Gestión de Gimnasio";
                Console.ForegroundColor = ConsoleColor.Cyan;

                // Inicialización del gestor de base de datos  
                var dbManager = new DatabaseManager();

                // Verificación de la conexión  
                if (!dbManager.TestConnection())
                {
                    Console.WriteLine("❌ Error: No se pudo conectar a la base de datos.");
                    Console.WriteLine("➡ Verifique la configuración e intente nuevamente.");
                    Console.WriteLine("\nPresione cualquier tecla para salir...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("✅ Conexión establecida con éxito.\n");

                // Inicialización de servicios  
                var servicioMiembro = new MiembroService(dbManager);
                var servicioInstructor = new InstructorService(dbManager);
                var servicioClase = new ClaseService(dbManager);
                var servicioMembresia = new MembresiaService(dbManager);
                var servicioReserva = new ReservaService(dbManager);

                // Inicialización del menú principal  
                var gestorMenu = new MenuManager(
                    servicioMiembro,
                    servicioInstructor,
                    servicioClase,
                    servicioMembresia,
                    servicioReserva
                );

                // Lanzar menú principal  
                gestorMenu.MostrarMenuPrincipal();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Se produjo un error inesperado: {ex.Message}");
                Console.WriteLine("➡ Por favor, revise el error e intente nuevamente.");
                Console.ResetColor();
                Console.WriteLine("\nPresione cualquier tecla para salir...");
                Console.ReadKey();
            }
        }
    }
}