using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GimnasioManager.Models;
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
                MessageBox.Show($"Instructor creado con ID: {instructor.ID_Instructor}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear instructor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Error al obtener instructores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Error al obtener instructor por ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(rows > 0 ? "Instructor actualizado." : "No se encontró el instructor para actualizar.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar instructor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(rows > 0 ? "Instructor eliminado." : "No se encontró el instructor para eliminar.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar instructor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}