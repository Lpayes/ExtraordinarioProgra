
using Microsoft.Data.SqlClient;
using GimnasioManager.Models;
using System;
using GimnasioManager.Utils;

namespace GimnasioManager.Services
{
    public class InstructorService
    {
        private readonly DatabaseManager _dbManager;

        public InstructorService(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        public void Crear(Instructor instructor)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand("INSERT INTO Instructores (Nombre, Apellido, Especialidad) VALUES (@Nombre, @Apellido, @Especialidad); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@Nombre", instructor.Nombre);
                command.Parameters.AddWithValue("@Apellido", instructor.Apellido);
                command.Parameters.AddWithValue("@Especialidad", instructor.Especialidad);

                instructor.ID_Instructor = Convert.ToInt32(command.ExecuteScalar());
                Console.WriteLine($"Instructor creado con ID: {instructor.ID_Instructor}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear instructor: {ex.Message}");
            }
        }

        public List<Instructor> ObtenerTodos()
        {
            var instructores = new List<Instructor>();
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand("SELECT ID_Instructor, Nombre, Apellido, Especialidad FROM Instructores", connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    instructores.Add(new Instructor
                    {
                        ID_Instructor = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Especialidad = reader.GetString(3)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener instructores: {ex.Message}");
            }
            return instructores;
        }

        public Instructor ObtenerPorId(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand("SELECT ID_Instructor, Nombre, Apellido, Especialidad FROM Instructores WHERE ID_Instructor = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Instructor
                    {
                        ID_Instructor = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Especialidad = reader.GetString(3)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener instructor por ID: {ex.Message}");
            }
            return null;
        }

        public void Actualizar(Instructor instructor)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand("UPDATE Instructores SET Nombre = @Nombre, Apellido = @Apellido, Especialidad = @Especialidad WHERE ID_Instructor = @ID_Instructor", connection);
                command.Parameters.AddWithValue("@ID_Instructor", instructor.ID_Instructor);
                command.Parameters.AddWithValue("@Nombre", instructor.Nombre);
                command.Parameters.AddWithValue("@Apellido", instructor.Apellido);
                command.Parameters.AddWithValue("@Especialidad", instructor.Especialidad);

                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Instructor actualizado." : "No se encontró el instructor para actualizar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar instructor: {ex.Message}");
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand("DELETE FROM Instructores WHERE ID_Instructor = @ID_Instructor", connection);
                command.Parameters.AddWithValue("@ID_Instructor", id);
                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Instructor eliminado." : "No se encontró el instructor para eliminar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar instructor: {ex.Message}");
            }
        }
    }
}