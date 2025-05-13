using System;
using System.Linq;
using GimnasioManager.Models;
using GimnasioManager.Services;

namespace GimnasioManager.UI
{
    public class MenuMembresias
    {
        private readonly MembresiaService _membresiaService;
        private readonly MiembroService _miembroService;
        private readonly ReservaService _reservaService;

        public MenuMembresias(MembresiaService membresiaService, MiembroService miembroService, ReservaService reservaService)
        {
            _membresiaService = membresiaService;
            _miembroService = miembroService;
            _reservaService = reservaService;
        }

        public void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE MEMBRESÍAS ===");
                Console.WriteLine("1. Registrar Membresía");
                Console.WriteLine("2. Listar Membresías");
                Console.WriteLine("3. Buscar Membresía por ID");
                Console.WriteLine("4. Actualizar Membresía");
                Console.WriteLine("5. Eliminar Membresía");
                Console.WriteLine("0. Volver al Menú Principal");
                Console.Write("\nSeleccione una opción: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        RegistrarMembresia();
                        break;
                    case "2":
                        ListarMembresias();
                        break;
                    case "3":
                        BuscarMembresiaPorId();
                        break;
                    case "4":
                        ActualizarMembresia();
                        break;
                    case "5":
                        EliminarMembresia();
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
        private void RegistrarMembresia()
        {
            Console.Clear();
            Console.WriteLine("REGISTRAR NUEVA MEMBRESÍA");

            string tipo;
            do
            {
                Console.Write("Tipo de Membresía (Mensual/Trimestral/Anual): ");
                tipo = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(tipo) ||
                    !(tipo.Equals("Mensual", StringComparison.OrdinalIgnoreCase) ||
                      tipo.Equals("Trimestral", StringComparison.OrdinalIgnoreCase) ||
                      tipo.Equals("Anual", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Tipo de membresía no válido.");
                    tipo = null;
                }
            } while (string.IsNullOrWhiteSpace(tipo));

            decimal precio;
            while (true)
            {
                Console.Write("Precio: ");
                if (decimal.TryParse(Console.ReadLine(), out precio) && precio >= 0)
                    break;
                Console.WriteLine("Precio inválido.");
            }

            DateTime fechaInicio;
            while (true)
            {
                Console.Write("Fecha de Inicio (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out fechaInicio))
                {
                    if (fechaInicio < new DateTime(2025, 1, 1))
                    {
                        Console.WriteLine("La fecha de inicio debe ser a partir del año 2025.");
                        continue;
                    }
                    break;
                }
                Console.WriteLine("Fecha inválida.");
            }

            DateTime fechaFin;
            switch (tipo.ToLower())
            {
                case "mensual":
                    fechaFin = fechaInicio.AddMonths(1);
                    break;
                case "trimestral":
                    fechaFin = fechaInicio.AddMonths(3);
                    break;
                case "anual":
                    fechaFin = fechaInicio.AddYears(1);
                    break;
                default:
                    Console.WriteLine("Tipo de membresía no válido.");
                    return;
            }
            // Ajuste de día si es necesario
            if (fechaFin.Day != fechaInicio.Day)
                fechaFin = new DateTime(fechaFin.Year, fechaFin.Month, DateTime.DaysInMonth(fechaFin.Year, fechaFin.Month));

            if (precio == 0)
            {
                Console.Write("El precio está en 0. ¿Desea continuar? (s/n): ");
                if (!Console.ReadLine().Trim().ToLower().Equals("s"))
                    return;
            }

            Console.WriteLine($"La membresía será válida desde {fechaInicio:dd/MM/yyyy} hasta {fechaFin:dd/MM/yyyy}.");
            Console.Write("¿Desea confirmar el registro? (s/n): ");
            if (!Console.ReadLine().Trim().ToLower().Equals("s"))
                return;

            var membresia = new Membresia
            {
                TipoMembresia = tipo,
                Precio = precio,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            try
            {
                _membresiaService.Crear(membresia);
                Console.WriteLine("¡Membresía registrada con éxito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar la membresía: {ex.Message}");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        private void ListarMembresias()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE MEMBRESÍAS ===");
            var membresias = _membresiaService.ObtenerTodos();
            if (membresias == null || !membresias.Any())
            {
                Console.WriteLine("No hay membresías registradas.");
            }
            else
            {
                foreach (var m in membresias)
                {
                    Console.WriteLine($"ID: {m.ID_Membresia}, Tipo: {m.TipoMembresia}, Precio: {m.Precio}, Inicio: {m.FechaInicio:yyyy-MM-dd}, Fin: {m.FechaFin:yyyy-MM-dd}");
                }
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarMembresiaPorId()
        {
            Console.Write("Ingrese el ID de la membresía: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var membresia = _membresiaService.ObtenerPorId(id);
                if (membresia != null)
                {
                    Console.WriteLine($"ID: {membresia.ID_Membresia}, Tipo: {membresia.TipoMembresia}, Precio: {membresia.Precio}, Inicio: {membresia.FechaInicio:yyyy-MM-dd}, Fin: {membresia.FechaFin:yyyy-MM-dd}");
                }
                else
                {
                    Console.WriteLine("No se encontró una membresía con ese ID.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarMembresia()
        {
            Console.Write("Ingrese el ID de la membresía a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var membresia = _membresiaService.ObtenerPorId(id);
            if (membresia == null)
            {
                Console.WriteLine("No se encontró una membresía con ese ID.");
                return;
            }

            // Validar reservas asociadas
            var miembro = _miembroService.ObtenerTodos().FirstOrDefault(m => m.ID_Membresia == membresia.ID_Membresia);
            if (miembro != null)
            {
                var reservas = _reservaService.ObtenerTodos()
                    .Where(r => r.ID_Miembro == miembro.ID_Miembro
                        && r.FechaReserva >= membresia.FechaInicio
                        && r.FechaReserva <= membresia.FechaFin)
                    .ToList();

                if (reservas.Any())
                {
                    Console.WriteLine("No se puede actualizar la membresía porque existen reservas asociadas a este miembro en el rango de fechas de la membresía.");
                    return;
                }
            }

            Console.Write($"Nuevo tipo ({membresia.TipoMembresia}): ");
            var tipo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(tipo))
            {
                if (!(tipo.Equals("Mensual", StringComparison.OrdinalIgnoreCase) ||
                      tipo.Equals("Trimestral", StringComparison.OrdinalIgnoreCase) ||
                      tipo.Equals("Anual", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Tipo de membresía no válido.");
                    return;
                }
                membresia.TipoMembresia = tipo;
            }

            Console.Write($"Nuevo precio ({membresia.Precio}): ");
            var precioStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(precioStr) && decimal.TryParse(precioStr, out decimal precio))
            {
                if (precio == 0)
                {
                    Console.Write("El precio está en 0. ¿Desea continuar? (s/n): ");
                    if (!Console.ReadLine().Trim().ToLower().Equals("s"))
                        return;
                }
                membresia.Precio = precio;
            }

            Console.Write($"Nueva fecha de inicio ({membresia.FechaInicio:yyyy-MM-dd}): ");
            var fechaInicioStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(fechaInicioStr) && DateTime.TryParse(fechaInicioStr, out DateTime fechaInicio))
            {
                if (fechaInicio < DateTime.Today)
                {
                    Console.WriteLine("La fecha de inicio no puede ser una fecha pasada.");
                    return;
                }
                membresia.FechaInicio = fechaInicio;
            }

            // Recalcular fecha de fin
            DateTime nuevaFechaFin;
            switch (membresia.TipoMembresia.ToLower())
            {
                case "mensual":
                    nuevaFechaFin = membresia.FechaInicio.AddMonths(1);
                    break;
                case "trimestral":
                    nuevaFechaFin = membresia.FechaInicio.AddMonths(3);
                    break;
                case "anual":
                    nuevaFechaFin = membresia.FechaInicio.AddYears(1);
                    break;
                default:
                    Console.WriteLine("Tipo de membresía no válido.");
                    return;
            }
            if (nuevaFechaFin.Day != membresia.FechaInicio.Day)
                nuevaFechaFin = new DateTime(nuevaFechaFin.Year, nuevaFechaFin.Month, DateTime.DaysInMonth(nuevaFechaFin.Year, nuevaFechaFin.Month));

            if (membresia.FechaFin >= DateTime.Today && nuevaFechaFin <= membresia.FechaFin)
            {
                Console.WriteLine("Solo puedes actualizar la membresía si la fecha de fin ya ha pasado o si la nueva fecha de fin es mayor a la actual.");
                return;
            }

            membresia.FechaFin = nuevaFechaFin;

            try
            {
                _membresiaService.Actualizar(membresia);
                Console.WriteLine("¡Membresía actualizada con éxito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la membresía: {ex.Message}");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void EliminarMembresia()
        {
            Console.Write("Ingrese el ID de la membresía a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var membresia = _membresiaService.ObtenerPorId(id);
            if (membresia == null)
            {
                Console.WriteLine("No se encontró una membresía con ese ID.");
                return;
            }

            var miembrosConMembresia = _miembroService.ObtenerTodos().Where(m => m.ID_Membresia == id).ToList();
            if (miembrosConMembresia.Any())
            {
                Console.WriteLine("No se puede eliminar la membresía porque hay miembros que la tienen asignada. Elimine o actualice primero esos miembros.");
                return;
            }

            Console.WriteLine($"¿Está seguro de que desea eliminar la membresía {membresia.TipoMembresia}? (s/n)");
            var confirmacion = Console.ReadLine();
            if (confirmacion?.Trim().ToLower() == "s")
            {
                _membresiaService.Eliminar(id);
                Console.WriteLine("¡Membresía eliminada con éxito!");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}