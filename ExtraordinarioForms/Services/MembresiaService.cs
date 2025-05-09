using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GimnasioManager.Models;
using GimnasioManager.Utils;

namespace GimnasioManager.Services
{
    public class MembresiaService
    {
        private readonly DatabaseManager _dbManager;

        public MembresiaService(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        // Crear una nueva membresía  
        public void Crear(Membresia membresia)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Membresias (TipoMembresia, Precio, FechaInicio, FechaFin) " +
                    "VALUES (@TipoMembresia, @Precio, @FechaInicio, @FechaFin); SELECT SCOPE_IDENTITY();",
                    connection
                );
                command.Parameters.AddWithValue("@TipoMembresia", membresia.TipoMembresia);
                command.Parameters.AddWithValue("@Precio", membresia.Precio);
                command.Parameters.AddWithValue("@FechaInicio", membresia.FechaInicio);
                command.Parameters.AddWithValue("@FechaFin", membresia.FechaFin);

                membresia.ID_Membresia = Convert.ToInt32(command.ExecuteScalar());
                Console.WriteLine("Membresía creada con éxito. ID: " + membresia.ID_Membresia);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear membresía: {ex.Message}");
            }
        }

        // Obtener todas las membresías  
        public List<Membresia> ObtenerTodos()
        {
            var membresias = new List<Membresia>();

            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();

                var command = new SqlCommand("SELECT ID_Membresia, TipoMembresia, Precio, FechaInicio, FechaFin FROM Membresias", connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    membresias.Add(new Membresia
                    {
                        ID_Membresia = reader.GetInt32(0),
                        TipoMembresia = reader.GetString(1),
                        Precio = reader.GetDecimal(2),
                        FechaInicio = reader.GetDateTime(3),
                        FechaFin = reader.GetDateTime(4)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener membresías: {ex.Message}");
            }

            return membresias;
        }

        // Obtener una membresía por ID  
        public Membresia ObtenerPorId(int id)
        {
            Membresia membresia = null;

            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();

                var command = new SqlCommand("SELECT ID_Membresia, TipoMembresia, Precio, FechaInicio, FechaFin FROM Membresias WHERE ID_Membresia = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    membresia = new Membresia
                    {
                        ID_Membresia = reader.GetInt32(0),
                        TipoMembresia = reader.GetString(1),
                        Precio = reader.GetDecimal(2),
                        FechaInicio = reader.GetDateTime(3),
                        FechaFin = reader.GetDateTime(4)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la membresía: {ex.Message}");
            }

            return membresia;
        }

        // Actualizar una membresía existente  
        public void Actualizar(Membresia membresia)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Membresias SET TipoMembresia = @TipoMembresia, Precio = @Precio, FechaInicio = @FechaInicio, FechaFin = @FechaFin " +
                    "WHERE ID_Membresia = @ID_Membresia",
                    connection
                );
                command.Parameters.AddWithValue("@ID_Membresia", membresia.ID_Membresia);
                command.Parameters.AddWithValue("@TipoMembresia", membresia.TipoMembresia);
                command.Parameters.AddWithValue("@Precio", membresia.Precio);
                command.Parameters.AddWithValue("@FechaInicio", membresia.FechaInicio);
                command.Parameters.AddWithValue("@FechaFin", membresia.FechaFin);

                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Membresía actualizada con éxito.");
                }
                else
                {
                    Console.WriteLine("No se encontró la membresía para actualizar.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar membresía: {ex.Message}");
            }
        }

        // Eliminar una membresía por ID  
        public void Eliminar(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand("DELETE FROM Membresias WHERE ID_Membresia = @ID_Membresia", connection);
                command.Parameters.AddWithValue("@ID_Membresia", id);

                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Membresía eliminada con éxito.");
                }
                else
                {
                    Console.WriteLine("No se encontró la membresía para eliminar.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar membresía: {ex.Message}");
            }
        }
    }
}
