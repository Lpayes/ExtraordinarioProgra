using System;
using System;
using System.Linq;
using GimnasioManager.Models;
using GimnasioManager.Services;

namespace GimnasioManager.UI
{
    public class MenuMiembros
    {
        private readonly MiembroService _miembroService;
        private readonly MembresiaService _membresiaService;
        private readonly ReservaService _reservaService;

        public MenuMiembros(MiembroService miembroService, MembresiaService membresiaService, ReservaService reservaService)
        {
            _miembroService = miembroService;
            _membresiaService = membresiaService;
            _reservaService = reservaService;
        }

        public void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE MIEMBROS ===");
                Console.WriteLine("1. Registrar Miembro");
                Console.WriteLine("2. Listar Miembros");
                Console.WriteLine("3. Buscar Miembro por Nombre");
                Console.WriteLine("4. Actualizar Miembro");
                Console.WriteLine("5. Eliminar Miembro");
                Console.WriteLine("0. Volver al Menú Principal");
                Console.Write("\nSeleccione una opción: ");
                switch (Console.ReadLine())
                {
                    case "1": RegistrarMiembro(); break;
                    case "2": ListarMiembros(); break;
                    case "3": BuscarMiembroPorNombre(); break;
                    case "4": ActualizarMiembro(); break;
                    case "5": EliminarMiembro(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void RegistrarMiembro()
        {
            Console.Clear();
            Console.WriteLine("REGISTRAR NUEVO MIEMBRO");

            string nombre, apellido, email, telefono;
            DateTime fechaNacimiento;
            int idMembresia = 0;

            do
            {
                Console.Write("Nombre: ");
                nombre = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nombre))
                    Console.WriteLine("El campo 'Nombre' es obligatorio.");
            } while (string.IsNullOrWhiteSpace(nombre));

            do
            {
                Console.Write("Apellido: ");
                apellido = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(apellido))
                    Console.WriteLine("El campo 'Apellido' es obligatorio.");
            } while (string.IsNullOrWhiteSpace(apellido));

            while (true)
            {
                Console.Write("Fecha de nacimiento (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out fechaNacimiento))
                {
                    int edad = DateTime.Today.Year - fechaNacimiento.Year;
                    if (fechaNacimiento > DateTime.Today.AddYears(-edad)) edad--;
                    if (edad < 14)
                    {
                        Console.WriteLine("Solo se pueden registrar personas mayores o iguales a 14 años.");
                        continue;
                    }
                    break;
                }
                Console.WriteLine("Fecha inválida.");
            }

            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
                    Console.WriteLine("El campo 'Email' es obligatorio y debe tener formato válido.");
            } while (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."));

            do
            {
                Console.Write("Teléfono (8 dígitos): ");
                telefono = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(telefono) || telefono.Length != 8 || !telefono.All(char.IsDigit))
                    Console.WriteLine("El teléfono debe contener exactamente 8 dígitos.");
            } while (string.IsNullOrWhiteSpace(telefono) || telefono.Length != 8 || !telefono.All(char.IsDigit));

            while (true)
            {
                Console.Write("ID de Membresía: ");
                var idMembresiaStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(idMembresiaStr) && int.TryParse(idMembresiaStr, out int idMembresiaVal))
                {
                    var membresiaExistente = _miembroService.ObtenerTodos().Any(m => m.ID_Membresia == idMembresiaVal);
                    if (membresiaExistente)
                    {
                        Console.WriteLine("El ID de membresía ya está asignado a otro miembro. Por favor, ingrese un ID único.");
                        continue;
                    }

                    var membresia = _membresiaService.ObtenerPorId(idMembresiaVal);
                    if (membresia == null)
                    {
                        Console.WriteLine("La membresía especificada no existe.");
                        continue;
                    }

                    idMembresia = idMembresiaVal;
                    break;
                }
                Console.WriteLine("Por favor, ingrese un ID de membresía válido.");
            }

            var miembro = new Miembro
            {
                Nombre = nombre,
                Apellido = apellido,
                FechaNacimiento = fechaNacimiento,
                Email = email,
                Telefono = telefono,
                ID_Membresia = idMembresia
            };

            try
            {
                _miembroService.Crear(miembro);
                Console.WriteLine("¡Miembro registrado con éxito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar el miembro: {ex.Message}");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ListarMiembros()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE MIEMBROS ===");
            var miembros = _miembroService.ObtenerTodos();
            if (miembros == null || !miembros.Any())
            {
                Console.WriteLine("No hay miembros registrados.");
            }
            else
            {
                foreach (var m in miembros)
                {
                    Console.WriteLine($"ID: {m.ID_Miembro}, Nombre: {m.Nombre}, Apellido: {m.Apellido}, Email: {m.Email}, Teléfono: {m.Telefono}, ID Membresía: {m.ID_Membresia}");
                }
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarMiembroPorNombre()
        {
            Console.Write("Ingrese el nombre del miembro a buscar: ");
            var nombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                var miembros = _miembroService.ObtenerTodos()
                    .Where(m => m.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (miembros.Any())
                {
                    Console.WriteLine("=== RESULTADOS DE LA BÚSQUEDA ===");
                    foreach (var m in miembros)
                    {
                        Console.WriteLine($"ID: {m.ID_Miembro}, Nombre: {m.Nombre}, Apellido: {m.Apellido}, Email: {m.Email}, Teléfono: {m.Telefono}, ID Membresía: {m.ID_Membresia}");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron miembros con ese nombre.");
                }
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un nombre válido.");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarMiembro()
        {
            Console.Write("Ingrese el ID del miembro a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var miembro = _miembroService.ObtenerPorId(id);
                if (miembro != null)
                {
                    Console.Write($"Nuevo nombre ({miembro.Nombre}): ");
                    var nombre = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nombre))
                        miembro.Nombre = nombre;

                    Console.Write($"Nuevo apellido ({miembro.Apellido}): ");
                    var apellido = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(apellido))
                        miembro.Apellido = apellido;

                    Console.Write($"Nueva fecha de nacimiento ({miembro.FechaNacimiento:yyyy-MM-dd}): ");
                    var fechaStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(fechaStr) && DateTime.TryParse(fechaStr, out DateTime fechaNacimiento))
                    {
                        int edad = DateTime.Today.Year - fechaNacimiento.Year;
                        if (fechaNacimiento > DateTime.Today.AddYears(-edad)) edad--;
                        if (edad < 14)
                        {
                            Console.WriteLine("Solo se pueden registrar personas mayores o iguales a 14 años.");
                            return;
                        }
                        miembro.FechaNacimiento = fechaNacimiento;
                    }

                    Console.Write($"Nuevo email ({miembro.Email}): ");
                    var email = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains("."))
                        miembro.Email = email;

                    Console.Write($"Nuevo teléfono ({miembro.Telefono}): ");
                    var telefono = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(telefono) && telefono.Length == 8 && telefono.All(char.IsDigit))
                        miembro.Telefono = telefono;

                    Console.WriteLine("El ID de membresía no puede ser actualizado, ya que cada miembro tiene una membresía única.");

                    _miembroService.Actualizar(miembro);
                    Console.WriteLine("¡Miembro actualizado con éxito!");
                }
                else
                {
                    Console.WriteLine("No se encontró un miembro con ese ID.");
                }
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un ID válido.");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void EliminarMiembro()
        {
            Console.Write("Ingrese el ID del miembro a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var reservas = _reservaService.ObtenerTodos().Where(r => r.ID_Miembro == id).ToList();
                if (reservas.Any())
                {
                    Console.WriteLine("No se puede eliminar el miembro porque tiene reservas asociadas. Elimine primero todas las reservas de este miembro.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                var miembro = _miembroService.ObtenerPorId(id);
                if (miembro == null)
                {
                    Console.WriteLine("No se encontró un miembro con ese ID.");
                }
                else
                {
                    _miembroService.Eliminar(id);
                    Console.WriteLine("¡Miembro eliminado con éxito!");
                }
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un ID válido.");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}