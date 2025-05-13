using System;
using System.Linq;
using GimnasioManager.Models;
using GimnasioManager.Services;
using GimnasioManager.Utils;

namespace GimnasioManager.UI
{
    public class MenuInstructores
    {
        private readonly InstructorService _instructorService;
        private readonly ClaseService _claseService;

        public MenuInstructores(InstructorService instructorService)
        {
            _instructorService = new InstructorService(new DatabaseManager());
            _claseService = new ClaseService(new DatabaseManager());
        }

        public void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE INSTRUCTORES ===");
                Console.WriteLine("1. Registrar Instructor");
                Console.WriteLine("2. Listar Instructores");
                Console.WriteLine("3. Buscar Instructor por Nombre");
                Console.WriteLine("4. Actualizar Instructor");
                Console.WriteLine("5. Eliminar Instructor");
                Console.WriteLine("0. Volver al Menú Principal");
                Console.Write("\nSeleccione una opción: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        RegistrarInstructor();
                        break;
                    case "2":
                        ListarInstructores();
                        break;
                    case "3":
                        BuscarInstructorPorNombre();
                        break;
                    case "4":
                        ActualizarInstructor();
                        break;
                    case "5":
                        EliminarInstructor();
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

        private void RegistrarInstructor()
        {
            string nombre, apellido, especialidad;

            // Nombre obligatorio
            do
            {
                Console.Write("Ingrese el nombre del instructor: ");
                nombre = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nombre))
                    Console.WriteLine("El campo 'Nombre' es obligatorio.");
            } while (string.IsNullOrWhiteSpace(nombre));

            // Apellido obligatorio
            do
            {
                Console.Write("Ingrese el apellido del instructor: ");
                apellido = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(apellido))
                    Console.WriteLine("El campo 'Apellido' es obligatorio.");
            } while (string.IsNullOrWhiteSpace(apellido));

            // Especialidad obligatoria
            do
            {
                Console.Write("Ingrese la especialidad del instructor: ");
                especialidad = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(especialidad))
                    Console.WriteLine("El campo 'Especialidad' es obligatorio.");
            } while (string.IsNullOrWhiteSpace(especialidad));

            var instructor = new Instructor
            {
                Nombre = nombre,
                Apellido = apellido,
                Especialidad = especialidad
            };

            _instructorService.Crear(instructor);
            Console.WriteLine("¡Instructor registrado con éxito!");
        }

        private void ListarInstructores()
        {
            try
            {
                var instructores = _instructorService.ObtenerTodos();
                if (instructores != null && instructores.Any())
                {
                    Console.WriteLine("=== LISTA DE INSTRUCTORES ===");
                    foreach (var instructor in instructores)
                    {
                        Console.WriteLine($"ID: {instructor.ID_Instructor}, Nombre: {instructor.Nombre}, Apellido: {instructor.Apellido}, Especialidad: {instructor.Especialidad}");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron instructores.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar los instructores: {ex.Message}");
            }
        }

        private void BuscarInstructorPorNombre()
        {
            Console.Write("Ingrese el nombre del instructor a buscar: ");
            var nombre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nombre))
            {
                try
                {
                    var instructores = _instructorService.ObtenerTodos()
                        .Where(i => i.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (instructores.Any())
                    {
                        Console.WriteLine("=== RESULTADOS DE LA BÚSQUEDA ===");
                        foreach (var instructor in instructores)
                        {
                            Console.WriteLine($"ID: {instructor.ID_Instructor}, Nombre: {instructor.Nombre}, Apellido: {instructor.Apellido}, Especialidad: {instructor.Especialidad}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron instructores con ese nombre.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al buscar el instructor: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un nombre válido.");
            }
        }

        private void ActualizarInstructor()
        {
            Console.Write("Ingrese el ID del instructor a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var instructor = _instructorService.ObtenerPorId(id);
                    if (instructor != null)
                    {
                        // Validar si se intenta cambiar la especialidad y el instructor tiene clases asignadas
                        var clasesAsignadas = _claseService.ObtenerTodos().Where(c => c.ID_Instructor == id).ToList();

                        Console.Write("Ingrese el nuevo nombre (deje en blanco para no cambiar): ");
                        var nombre = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nombre))
                        {
                            instructor.Nombre = nombre;
                        }

                        Console.Write("Ingrese el nuevo apellido (deje en blanco para no cambiar): ");
                        var apellido = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(apellido))
                        {
                            instructor.Apellido = apellido;
                        }

                        Console.Write("Ingrese la nueva especialidad (deje en blanco para no cambiar): ");
                        var especialidad = Console.ReadLine();
                        string nuevaEspecialidad = !string.IsNullOrWhiteSpace(especialidad) ? especialidad : instructor.Especialidad;

                        if (clasesAsignadas.Any() && !string.Equals(instructor.Especialidad, nuevaEspecialidad, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("No se puede cambiar la especialidad porque el instructor tiene clases asignadas. Elimine primero todas las clases asociadas.");
                            return;
                        }

                        instructor.Especialidad = nuevaEspecialidad;

                        _instructorService.Actualizar(instructor);
                        Console.WriteLine("¡Instructor actualizado con éxito!");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró un instructor con el ID proporcionado.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar el instructor: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un ID válido.");
            }
        }

        private void EliminarInstructor()
        {
            Console.Write("Ingrese el ID del instructor a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var instructor = _instructorService.ObtenerPorId(id);
                    if (instructor != null)
                    {
                        var clasesAsignadas = _claseService.ObtenerTodos().Where(c => c.ID_Instructor == id).ToList();
                        if (clasesAsignadas.Any())
                        {
                            Console.WriteLine("No se puede eliminar el instructor porque tiene clases asignadas. Elimine primero todas las clases asociadas.");
                            return;
                        }

                        Console.WriteLine($"¿Está seguro de que desea eliminar al instructor {instructor.Nombre}? (s/n)");
                        var confirmacion = Console.ReadLine();
                        if (confirmacion?.Trim().ToLower() == "s")
                        {
                            _instructorService.Eliminar(id);
                            Console.WriteLine("¡Instructor eliminado con éxito!");
                        }
                        else
                        {
                            Console.WriteLine("Operación cancelada.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontró un instructor con el ID proporcionado.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al eliminar el instructor: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un ID válido.");
            }
        }
    }
}
