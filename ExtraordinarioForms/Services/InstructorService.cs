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
        public static readonly List<string> EspecialidadesPermitidas = new List<string>
           {
               "Yoga",
               "Spinning",
               "CrossFit",
               "Boxeo"
           };
        public InstructorService(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        public void Crear(Instructor instructor)
        {
            try
            {
                if (!EspecialidadesPermitidas.Contains(instructor.Especialidad, StringComparer.OrdinalIgnoreCase))
                {
                    MessageBox.Show("No se necesita un instructor con esta especialidad por el momento. Solo necesitamos personas con las siguientes especialidades: Yoga, Spinning, CrossFit, Boxeo.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var connection = _dbManager.GetConnection();
                connection.Open();

                var command = new SqlCommand(
                    "INSERT INTO Instructores (Nombre, Apellido, Especialidad) " +
                    "VALUES (@Nombre, @Apellido, @Especialidad); SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@Nombre", instructor.Nombre);
                command.Parameters.AddWithValue("@Apellido", instructor.Apellido);
                command.Parameters.AddWithValue("@Especialidad", instructor.Especialidad);

                instructor.ID_Instructor = Convert.ToInt32(command.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw;
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
                throw;
            }
            return instructores;
        }
        public Instructor ObtenerPorId(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();

                var command = new SqlCommand(
                    "SELECT ID_Instructor, Nombre, Apellido, Especialidad FROM Instructores WHERE ID_Instructor = @Id", connection);

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
                throw;
            }
            return null;
        }

        public void ActualizarInstructor(Instructor instructor)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(@"
                    UPDATE Instructores
                    SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono, Especialidad = @Especialidad
                    WHERE ID_Instructor = @ID_Instructor", connection);

                command.Parameters.AddWithValue("@ID_Instructor", instructor.ID_Instructor);
                command.Parameters.AddWithValue("@Nombre", instructor.Nombre);
                command.Parameters.AddWithValue("@Apellido", instructor.Apellido);
                command.Parameters.AddWithValue("@Especialidad", instructor.Especialidad);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
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

                command.ExecuteNonQuery();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new InvalidOperationException("No se puede eliminar el instructor porque tiene clases asociadas. Elimine las clases primero.", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}