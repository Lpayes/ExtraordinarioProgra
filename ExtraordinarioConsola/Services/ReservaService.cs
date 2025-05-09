using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GimnasioManager.Models;
using GimnasioManager.Utils;

namespace GimnasioManager.Services
{
    public class ReservaService
    {
        private readonly DatabaseManager _dbManager;

        public ReservaService(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        // Crear una nueva reserva
        public void Crear(Reserva reserva)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Reservas (ID_Miembro, ID_Clase, FechaReserva) 
                    VALUES (@ID_Miembro, @ID_Clase, @FechaReserva); 
                    SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@ID_Miembro", reserva.ID_Miembro);
                command.Parameters.AddWithValue("@ID_Clase", reserva.ID_Clase);
                command.Parameters.AddWithValue("@FechaReserva", reserva.FechaReserva);

                reserva.ID_Reserva = Convert.ToInt32(command.ExecuteScalar());
                Console.WriteLine($"Reserva creada con ID: {reserva.ID_Reserva}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear reserva: {ex.Message}");
            }
        }

        // Obtener todas las reservas con detalles de Miembros y Clases
        public List<Reserva> ObtenerTodos()
        {
            var reservas = new List<Reserva>();
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(@"
                    SELECT r.ID_Reserva, r.ID_Miembro, m.Nombre AS NombreMiembro, m.Apellido AS ApellidoMiembro, 
                           r.ID_Clase, c.NombreClase, r.FechaReserva
                    FROM Reservas r
                    JOIN Miembros m ON r.ID_Miembro = m.ID_Miembro
                    JOIN Clases c ON r.ID_Clase = c.ID_Clase", connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    reservas.Add(new Reserva
                    {
                        ID_Reserva = reader.GetInt32(0),
                        ID_Miembro = reader.GetInt32(1),
                        NombreMiembro = reader.GetString(2) + " " + reader.GetString(3), // Nombre completo del miembro
                        ID_Clase = reader.GetInt32(4),
                        NombreClase = reader.GetString(5), // Nombre de la clase
                        FechaReserva = reader.GetDateTime(6)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener reservas: {ex.Message}");
            }
            return reservas;
        }

        // Obtener una reserva por ID con detalles de Miembros y Clases
        public Reserva ObtenerPorId(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(@"
                    SELECT r.ID_Reserva, r.ID_Miembro, m.Nombre AS NombreMiembro, m.Apellido AS ApellidoMiembro, 
                           r.ID_Clase, c.NombreClase, r.FechaReserva
                    FROM Reservas r
                    JOIN Miembros m ON r.ID_Miembro = m.ID_Miembro
                    JOIN Clases c ON r.ID_Clase = c.ID_Clase
                    WHERE r.ID_Reserva = @Id", connection);

                command.Parameters.AddWithValue("@Id", id);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Reserva
                    {
                        ID_Reserva = reader.GetInt32(0),
                        ID_Miembro = reader.GetInt32(1),
                        NombreMiembro = reader.GetString(2) + " " + reader.GetString(3), // Nombre completo del miembro
                        ID_Clase = reader.GetInt32(4),
                        NombreClase = reader.GetString(5), // Nombre de la clase
                        FechaReserva = reader.GetDateTime(6)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener reserva por ID: {ex.Message}");
            }
            return null;
        }

        // Actualizar una reserva existente
        public void Actualizar(Reserva reserva)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(@"
                    UPDATE Reservas 
                    SET ID_Miembro = @ID_Miembro, ID_Clase = @ID_Clase, FechaReserva = @FechaReserva 
                    WHERE ID_Reserva = @ID_Reserva", connection);

                command.Parameters.AddWithValue("@ID_Reserva", reserva.ID_Reserva);
                command.Parameters.AddWithValue("@ID_Miembro", reserva.ID_Miembro);
                command.Parameters.AddWithValue("@ID_Clase", reserva.ID_Clase);
                command.Parameters.AddWithValue("@FechaReserva", reserva.FechaReserva);

                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Reserva actualizada." : "No se encontró la reserva para actualizar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar reserva: {ex.Message}");
            }
        }

        // Eliminar una reserva por ID
        public void Eliminar(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand("DELETE FROM Reservas WHERE ID_Reserva = @ID_Reserva", connection);
                command.Parameters.AddWithValue("@ID_Reserva", id);
                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Reserva eliminada." : "No se encontró la reserva para eliminar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar reserva: {ex.Message}");
            }
        }
    }
}