using System;
using System.Linq;
using GimnasioManager.Models;
using GimnasioManager.Services;

namespace GimnasioManager.UI
{
    public class MenuReservas
    {
        private readonly ReservaService _reservaService;
        private readonly MiembroService _miembroService;
        private readonly ClaseService _claseService;
        private readonly MembresiaService _membresiaService;

        public MenuReservas(
            ReservaService reservaService,
            MiembroService miembroService,
            ClaseService claseService,
            MembresiaService membresiaService)
        {
            _reservaService = reservaService;
            _miembroService = miembroService;
            _claseService = claseService;
            _membresiaService = membresiaService;
        }

        public void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE RESERVAS ===");
                Console.WriteLine("1. Crear Reserva");
                Console.WriteLine("2. Listar Reservas");
                Console.WriteLine("3. Buscar Reserva por ID");
                Console.WriteLine("4. Actualizar Reserva");
                Console.WriteLine("5. Eliminar Reserva");
                Console.WriteLine("0. Volver al Menú Principal");
                Console.Write("\nSeleccione una opción: ");
                switch (Console.ReadLine())
                {
                    case "1": CrearReserva(); break;
                    case "2": ListarReservas(); break;
                    case "3": BuscarReservaPorId(); break;
                    case "4": ActualizarReserva(); break;
                    case "5": EliminarReserva(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CrearReserva()
        {
            Console.Clear();
            Console.WriteLine("CREAR NUEVA RESERVA");

            int idMiembro;
            Miembro miembro;
            while (true)
            {
                Console.Write("ID del Miembro: ");
                if (int.TryParse(Console.ReadLine(), out idMiembro))
                {
                    miembro = _miembroService.ObtenerPorId(idMiembro);
                    if (miembro != null)
                        break;
                    Console.WriteLine("Miembro no encontrado.");
                }
                else
                {
                    Console.WriteLine("ID inválido.");
                }
            }

            int idClase;
            Clase clase;
            while (true)
            {
                Console.Write("ID de la Clase: ");
                if (int.TryParse(Console.ReadLine(), out idClase))
                {
                    clase = _claseService.ObtenerPorId(idClase);
                    if (clase != null)
                        break;
                    Console.WriteLine("Clase no encontrada.");
                }
                else
                {
                    Console.WriteLine("ID inválido.");
                }
            }

            DateTime fechaReserva;
            while (true)
            {
                Console.Write("Fecha de la Reserva (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out fechaReserva))
                {
                    if (fechaReserva < DateTime.Today)
                    {
                        Console.WriteLine("No se puede reservar en una fecha pasada.");
                        continue;
                    }
                    break;
                }
                Console.WriteLine("Fecha inválida.");
            }

            // Validar membresía vigente
            var membresia = _membresiaService.ObtenerPorId(miembro.ID_Membresia);
            if (membresia == null || fechaReserva < membresia.FechaInicio || fechaReserva > membresia.FechaFin)
            {
                Console.WriteLine("El miembro no tiene una membresía vigente para la fecha de la reserva.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Validar capacidad de la clase
            var reservasEnClase = _reservaService.ObtenerTodos()
                .Count(r => r.ID_Clase == idClase && r.FechaReserva.Date == fechaReserva.Date);
            if (reservasEnClase >= clase.CapacidadMaxima)
            {
                Console.WriteLine("La clase ya está llena para esa fecha.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Validar duplicidad de reserva
            var reservaExistente = _reservaService.ObtenerTodos()
                .Any(r => r.ID_Miembro == idMiembro && r.ID_Clase == idClase && r.FechaReserva.Date == fechaReserva.Date);
            if (reservaExistente)
            {
                Console.WriteLine("Ya existe una reserva para este miembro en esta clase y fecha.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            var reserva = new Reserva
            {
                ID_Miembro = idMiembro,
                ID_Clase = idClase,
                FechaReserva = fechaReserva
            };

            try
            {
                _reservaService.Crear(reserva);
                Console.WriteLine("¡Reserva creada con éxito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la reserva: {ex.Message}");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ListarReservas()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE RESERVAS ===");
            var reservas = _reservaService.ObtenerTodos();
            if (reservas == null || !reservas.Any())
            {
                Console.WriteLine("No hay reservas registradas.");
            }
            else
            {
                foreach (var r in reservas)
                {
                    Console.WriteLine($"ID: {r.ID_Reserva}, Miembro: {r.ID_Miembro}, Clase: {r.ID_Clase}, Fecha: {r.FechaReserva:yyyy-MM-dd}");
                }
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarReservaPorId()
        {
            Console.Write("Ingrese el ID de la reserva: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var reserva = _reservaService.ObtenerPorId(id);
                if (reserva != null)
                {
                    Console.WriteLine($"ID: {reserva.ID_Reserva}, Miembro: {reserva.ID_Miembro}, Clase: {reserva.ID_Clase}, Fecha: {reserva.FechaReserva:yyyy-MM-dd}");
                }
                else
                {
                    Console.WriteLine("No se encontró una reserva con ese ID.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarReserva()
        {
            Console.Write("Ingrese el ID de la reserva a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var reserva = _reservaService.ObtenerPorId(id);
            if (reserva == null)
            {
                Console.WriteLine("No se encontró una reserva con ese ID.");
                return;
            }

            Console.Write($"Nuevo ID de miembro ({reserva.ID_Miembro}): ");
            var miembroStr = Console.ReadLine();
            int idMiembro = reserva.ID_Miembro;
            if (!string.IsNullOrWhiteSpace(miembroStr) && int.TryParse(miembroStr, out int nuevoMiembro))
            {
                var miembro = _miembroService.ObtenerPorId(nuevoMiembro);
                if (miembro == null)
                {
                    Console.WriteLine("Miembro no encontrado.");
                    return;
                }
                idMiembro = nuevoMiembro;
            }

            Console.Write($"Nuevo ID de clase ({reserva.ID_Clase}): ");
            var claseStr = Console.ReadLine();
            int idClase = reserva.ID_Clase;
            if (!string.IsNullOrWhiteSpace(claseStr) && int.TryParse(claseStr, out int nuevaClase))
            {
                var clase = _claseService.ObtenerPorId(nuevaClase);
                if (clase == null)
                {
                    Console.WriteLine("Clase no encontrada.");
                    return;
                }
                idClase = nuevaClase;
            }

            Console.Write($"Nueva fecha de reserva ({reserva.FechaReserva:yyyy-MM-dd}): ");
            var fechaStr = Console.ReadLine();
            DateTime fechaReserva = reserva.FechaReserva;
            if (!string.IsNullOrWhiteSpace(fechaStr) && DateTime.TryParse(fechaStr, out DateTime nuevaFecha))
            {
                if (nuevaFecha < DateTime.Today)
                {
                    Console.WriteLine("No se puede reservar en una fecha pasada.");
                    return;
                }
                fechaReserva = nuevaFecha;
            }

            // Validar membresía vigente
            var miembroValidar = _miembroService.ObtenerPorId(idMiembro);
            var membresia = _membresiaService.ObtenerPorId(miembroValidar.ID_Membresia);
            if (membresia == null || fechaReserva < membresia.FechaInicio || fechaReserva > membresia.FechaFin)
            {
                Console.WriteLine("El miembro no tiene una membresía vigente para la fecha de la reserva.");
                return;
            }

            // Validar capacidad de la clase
            var claseValidar = _claseService.ObtenerPorId(idClase);
            var reservasEnClase = _reservaService.ObtenerTodos()
                .Count(r => r.ID_Clase == idClase && r.FechaReserva.Date == fechaReserva.Date && r.ID_Reserva != id);
            if (reservasEnClase >= claseValidar.CapacidadMaxima)
            {
                Console.WriteLine("La clase ya está llena para esa fecha.");
                return;
            }

            // Validar duplicidad de reserva
            var reservaExistente = _reservaService.ObtenerTodos()
                .Any(r => r.ID_Reserva != id && r.ID_Miembro == idMiembro && r.ID_Clase == idClase && r.FechaReserva.Date == fechaReserva.Date);
            if (reservaExistente)
            {
                Console.WriteLine("Ya existe una reserva para este miembro en esta clase y fecha.");
                return;
            }

            reserva.ID_Miembro = idMiembro;
            reserva.ID_Clase = idClase;
            reserva.FechaReserva = fechaReserva;

            try
            {
                _reservaService.Actualizar(reserva);
                Console.WriteLine("¡Reserva actualizada con éxito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la reserva: {ex.Message}");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void EliminarReserva()
        {
            Console.Write("Ingrese el ID de la reserva a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var reserva = _reservaService.ObtenerPorId(id);
            if (reserva == null)
            {
                Console.WriteLine("No se encontró una reserva con ese ID.");
                return;
            }

            Console.WriteLine($"¿Está seguro de que desea eliminar la reserva? (s/n)");
            var confirmacion = Console.ReadLine();
            if (confirmacion?.Trim().ToLower() == "s")
            {
                _reservaService.Eliminar(id);
                Console.WriteLine("¡Reserva eliminada con éxito!");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
