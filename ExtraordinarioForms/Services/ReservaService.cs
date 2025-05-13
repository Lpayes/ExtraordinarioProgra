using GimnasioManager.Models;
using GimnasioManager.Utils;
using Microsoft.Data.SqlClient;
using System;



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
            }
            catch (Exception ex)
            {
                throw;
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
                throw;
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
                throw;
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
               
            }
            catch (Exception ex)
            {
                throw;
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
                MessageBox.Show(rows > 0 ? "Reserva eliminada." : "No se encontró la reserva para eliminar.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal object ObtenerMiembroPorId(int idMiembro)
        {
            throw new NotImplementedException();
        }
    }
}