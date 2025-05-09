using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GimnasioManager.Models;
using GimnasioManager.Utils;

namespace GimnasioManager.Services
{
    public class ClaseService
    {
        private readonly DatabaseManager _dbManager;

        public ClaseService(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        // Crear una nueva clase
        public void Crear(Clase clase)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();

                var command = new SqlCommand(
                    "INSERT INTO Clases (NombreClase, CapacidadMaxima, Horario, ID_Instructor) " +
                    "VALUES (@NombreClase, @CapacidadMaxima, @Horario, @ID_Instructor); SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@NombreClase", clase.NombreClase);
                command.Parameters.AddWithValue("@CapacidadMaxima", clase.CapacidadMaxima);
                command.Parameters.AddWithValue("@Horario", clase.Horario);
                command.Parameters.AddWithValue("@ID_Instructor", clase.ID_Instructor);

                clase.ID_Clase = Convert.ToInt32(command.ExecuteScalar());
                Console.WriteLine($"Clase creada con ID: {clase.ID_Clase}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear clase: {ex.Message}");
            }
        }

        // Obtener todas las clases con detalles del instructor
        public List<Clase> ObtenerTodos()
        {
            var clases = new List<Clase>();
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();

                var command = new SqlCommand(@"
                    SELECT c.ID_Clase, c.NombreClase, c.CapacidadMaxima, c.Horario, c.ID_Instructor, 
                           i.Nombre + ' ' + i.Apellido AS NombreInstructor
                    FROM Clases c
                    JOIN Instructores i ON c.ID_Instructor = i.ID_Instructor", connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clases.Add(new Clase
                    {
                        ID_Clase = reader.GetInt32(0),
                        NombreClase = reader.GetString(1),
                        CapacidadMaxima = reader.GetInt32(2),
                        Horario = reader.GetTimeSpan(3),
                        ID_Instructor = reader.GetInt32(4),
                        NombreInstructor = reader.GetString(5) // Nombre completo del instructor
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clases: {ex.Message}");
            }

            return clases;
        }

        // Obtener una clase por su ID con detalles del instructor
        public Clase ObtenerPorId(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();

                var command = new SqlCommand(@"
                    SELECT c.ID_Clase, c.NombreClase, c.CapacidadMaxima, c.Horario, c.ID_Instructor, 
                           i.Nombre + ' ' + i.Apellido AS NombreInstructor
                    FROM Clases c
                    JOIN Instructores i ON c.ID_Instructor = i.ID_Instructor
                    WHERE c.ID_Clase = @Id", connection);

                command.Parameters.AddWithValue("@Id", id);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Clase
                    {
                        ID_Clase = reader.GetInt32(0),
                        NombreClase = reader.GetString(1),
                        CapacidadMaxima = reader.GetInt32(2),
                        Horario = reader.GetTimeSpan(3),
                        ID_Instructor = reader.GetInt32(4),
                        NombreInstructor = reader.GetString(5) // Nombre completo del instructor
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clase por ID: {ex.Message}");
            }

            return null;
        }

        // Actualizar una clase existente
        public void Actualizar(Clase clase)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();

                var command = new SqlCommand(
                    "UPDATE Clases SET NombreClase = @NombreClase, CapacidadMaxima = @CapacidadMaxima, Horario = @Horario, ID_Instructor = @ID_Instructor " +
                    "WHERE ID_Clase = @ID_Clase", connection);

                command.Parameters.AddWithValue("@ID_Clase", clase.ID_Clase);
                command.Parameters.AddWithValue("@NombreClase", clase.NombreClase);
                command.Parameters.AddWithValue("@CapacidadMaxima", clase.CapacidadMaxima);
                command.Parameters.AddWithValue("@Horario", clase.Horario);
                command.Parameters.AddWithValue("@ID_Instructor", clase.ID_Instructor);

                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Clase actualizada exitosamente." : "No se encontró la clase a actualizar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar clase: {ex.Message}");
            }
        }

        // Eliminar una clase por su ID
        public void Eliminar(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();

                var command = new SqlCommand("DELETE FROM Clases WHERE ID_Clase = @ID_Clase", connection);
                command.Parameters.AddWithValue("@ID_Clase", id);

                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Clase eliminada exitosamente." : "No se encontró la clase a eliminar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar clase: {ex.Message}");
            }
        }
    }
}