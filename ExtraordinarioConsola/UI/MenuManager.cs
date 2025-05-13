using System;
using GimnasioManager.Services;
using GimnasioManager.Utils;

namespace GimnasioManager.UI
{
    public class MenuManager1
    {
        private readonly MenuMiembros _menuMiembros;
        private readonly MenuInstructores _menuInstructores;
        private readonly MenuClases _menuClases;
        private readonly MenuMembresias _menuMembresias;
        private readonly MenuReservas _menuReservas;

        public MenuManager1()
        {
            // Instancia única de DatabaseManager para todos los servicios
            var dbManager = new DatabaseManager();

            // Instanciar servicios
            var miembroService = new MiembroService(dbManager);
            var membresiaService = new MembresiaService(dbManager);
            var reservaService = new ReservaService(dbManager);
            var instructorService = new InstructorService(dbManager);
            var claseService = new ClaseService(dbManager);

            // Instanciar menús
            _menuMiembros = new MenuMiembros(miembroService, membresiaService, reservaService);
            _menuInstructores = new MenuInstructores(instructorService);
            _menuClases = new MenuClases(claseService, instructorService);
            _menuMembresias = new MenuMembresias(membresiaService, miembroService, reservaService);
            _menuReservas = new MenuReservas(reservaService, miembroService, claseService, membresiaService);
        }

        public void MostrarMenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ PRINCIPAL GIMNASIO ===");
                Console.WriteLine("1. Gestión de Miembros");
                Console.WriteLine("2. Gestión de Instructores");
                Console.WriteLine("3. Gestión de Clases");
                Console.WriteLine("4. Gestión de Membresías");
                Console.WriteLine("5. Gestión de Reservas");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        _menuMiembros.MostrarMenu();
                        break;
                    case "2":
                        _menuInstructores.MostrarMenu();
                        break;
                    case "3":
                        _menuClases.MostrarMenu();
                        break;
                    case "4":
                        _menuMembresias.MostrarMenu();
                        break;
                    case "5":
                        _menuReservas.MostrarMenu();
                        break;
                    case "0":
                        Console.WriteLine("¡Hasta luego!");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}