using GimnasioManager.Models;
using GimnasioManager.Services;

namespace GimnasioManager.UI
{
    public class MenuClases
    {
        private readonly ClaseService _claseService;
        private readonly InstructorService _instructorService;

        public MenuClases(ClaseService claseService, InstructorService instructorService)
        {
            _claseService = claseService;
            _instructorService = instructorService;
        }

        public void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE CLASES ===");
                Console.WriteLine("1. Registrar Clase");
                Console.WriteLine("2. Listar Clases");
                Console.WriteLine("3. Buscar Clase por Nombre");
                Console.WriteLine("4. Actualizar Clase");
                Console.WriteLine("5. Eliminar Clase");
                Console.WriteLine("0. Volver al Menú Principal");
                Console.Write("\nSeleccione una opción: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        RegistrarClase();
                        break;
                    case "2":
                        ListarClases();
                        break;
                    case "3":
                        BuscarClasePorNombre();
                        break;
                    case "4":
                        ActualizarClase();
                        break;
                    case "5":
                        EliminarClase();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("\nOpción inválida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void RegistrarClase()
        {
            Console.Clear();
            Console.WriteLine("REGISTRAR NUEVA CLASE");

            string nombreClase;
            do
            {
                Console.Write("Nombre de la clase: ");
                nombreClase = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nombreClase))
                    Console.WriteLine("El nombre es obligatorio.");
            } while (string.IsNullOrWhiteSpace(nombreClase));

            TimeSpan horario;
            while (true)
            {
                Console.Write("Horario (HH:mm): ");
                if (TimeSpan.TryParse(Console.ReadLine(), out horario))
                    break;
                Console.WriteLine("Horario inválido. Intente de nuevo.");
            }

            int capacidadMaxima;
            while (true)
            {
                Console.Write("Capacidad máxima: ");
                if (int.TryParse(Console.ReadLine(), out capacidadMaxima) && capacidadMaxima > 0)
                    break;
                Console.WriteLine("Capacidad máxima inválida. Debe ser un número mayor a 0.");
            }

            int idInstructor;
            Instructor instructor;
            while (true)
            {
                Console.Write("ID del Instructor: ");
                if (int.TryParse(Console.ReadLine(), out idInstructor))
                {
                    instructor = _instructorService.ObtenerPorId(idInstructor);
                    if (instructor != null)
                        break;
                    Console.WriteLine("Instructor no encontrado. Intente con un ID válido.");
                }
                else
                {
                    Console.WriteLine("ID inválido.");
                }
            }

            int idMembresia;
            while (true)
            {
                Console.Write("ID de la Membresía: ");
                if (int.TryParse(Console.ReadLine(), out idMembresia) && idMembresia > 0)
                    break;
                Console.WriteLine("El ID de la Membresía es obligatorio y debe ser un número mayor a 0.");
            }

            var clase = new Clase
            {
                NombreClase = nombreClase,
                Horario = horario,
                CapacidadMaxima = capacidadMaxima,
                ID_Instructor = idInstructor
            };

            try
            {
                _claseService.Crear(clase);
                Console.WriteLine("\n¡Clase registrada con éxito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al registrar la clase: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        private void ListarClases()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE CLASES ===");
            var clases = _claseService.ObtenerTodos();

            if (clases == null || !clases.Any())
            {
                Console.WriteLine("No hay clases registradas.");
            }
            else
            {
                foreach (var clase in clases)
                {
                    Console.WriteLine($"ID: {clase.ID_Clase}, Nombre: {clase.NombreClase}, Horario: {clase.Horario}, Capacidad: {clase.CapacidadMaxima}, ID Instructor: {clase.ID_Instructor}");
                }
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarClasePorNombre()
        {
            Console.Write("Ingrese el nombre de la clase a buscar: ");
            var nombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                var clases = _claseService.ObtenerTodos()
                    .Where(c => c.NombreClase.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (clases.Any())
                {
                    Console.WriteLine("=== RESULTADOS DE LA BÚSQUEDA ===");
                    foreach (var clase in clases)
                    {
                        Console.WriteLine($"ID: {clase.ID_Clase}, Nombre: {clase.NombreClase}, Horario: {clase.Horario}, Capacidad: {clase.CapacidadMaxima}, ID Instructor: {clase.ID_Instructor}");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron clases con ese nombre.");
                }
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un nombre válido.");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarClase()
        {
            Console.Write("Ingrese el ID de la clase a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var clase = _claseService.ObtenerPorId(id);
                if (clase != null)
                {
                    Console.Write($"Nuevo nombre ({clase.NombreClase}): ");
                    var nombre = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nombre))
                        clase.NombreClase = nombre;

                    Console.Write($"Nuevo horario ({clase.Horario}): ");
                    var horarioStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(horarioStr) && TimeSpan.TryParse(horarioStr, out TimeSpan horario))
                        clase.Horario = horario;

                    Console.Write($"Nueva capacidad máxima ({clase.CapacidadMaxima}): ");
                    var capacidadStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(capacidadStr) && int.TryParse(capacidadStr, out int capacidad) && capacidad > 0)
                        clase.CapacidadMaxima = capacidad;

                    Console.Write($"Nuevo ID de instructor ({clase.ID_Instructor}): ");
                    var idInstructorStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(idInstructorStr) && int.TryParse(idInstructorStr, out int idInstructor))
                    {
                        var instructor = _instructorService.ObtenerPorId(idInstructor);
                        if (instructor != null)
                        {
                            clase.ID_Instructor = idInstructor;
                        }
                        else
                        {
                            Console.WriteLine("Instructor no encontrado. No se actualizó el ID de instructor.");
                        }
                    }

                    _claseService.Actualizar(clase);
                    Console.WriteLine("¡Clase actualizada con éxito!");
                }
                else
                {
                    Console.WriteLine("No se encontró una clase con ese ID.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void EliminarClase()
        {
            Console.Write("Ingrese el ID de la clase a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var clase = _claseService.ObtenerPorId(id);
                if (clase != null)
                {
                    Console.WriteLine($"¿Está seguro de que desea eliminar la clase {clase.NombreClase}? (s/n)");
                    var confirmacion = Console.ReadLine();
                    if (confirmacion?.Trim().ToLower() == "s")
                    {
                        _claseService.Eliminar(id);
                        Console.WriteLine("¡Clase eliminada con éxito!");
                    }
                    else
                    {
                        Console.WriteLine("Operación cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró una clase con ese ID.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}