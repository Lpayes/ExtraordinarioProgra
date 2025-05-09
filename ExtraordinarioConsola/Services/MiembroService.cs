using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GimnasioManager.Models;
using GimnasioManager.Utils;

namespace GimnasioManager.Services
{
    public class MiembroService
    {
        private readonly DatabaseManager _dbManager;

        public MiembroService(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        // Crear un nuevo miembro
        public void Crear(Miembro miembro)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Miembros (Nombre, Apellido, FechaNacimiento, Email, Telefono, ID_Membresia)
                    VALUES (@Nombre, @Apellido, @FechaNacimiento, @Email, @Telefono, @ID_Membresia);
                    SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@Nombre", miembro.Nombre);
                command.Parameters.AddWithValue("@Apellido", miembro.Apellido);
                command.Parameters.AddWithValue("@FechaNacimiento", miembro.FechaNacimiento);
                command.Parameters.AddWithValue("@Email", miembro.Email);
                command.Parameters.AddWithValue("@Telefono", miembro.Telefono);
                command.Parameters.AddWithValue("@ID_Membresia", miembro.ID_Membresia);

                miembro.ID_Miembro = Convert.ToInt32(command.ExecuteScalar());
                Console.WriteLine($"Miembro creado con ID: {miembro.ID_Miembro}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear miembro: {ex.Message}");
            }
        }

        // Obtener todos los miembros
        public List<Miembro> ObtenerTodos()
        {
            var miembros = new List<Miembro>();
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(@"
                    SELECT ID_Miembro, Nombre, Apellido, FechaNacimiento, Email, Telefono, ID_Membresia 
                    FROM Miembros", connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    miembros.Add(new Miembro
                    {
                        ID_Miembro = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        FechaNacimiento = reader.GetDateTime(3),
                        Email = reader.GetString(4),
                        Telefono = reader.GetString(5),
                        ID_Membresia = reader.GetInt32(6)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener miembros: {ex.Message}");
            }
            return miembros;
        }

        // Obtener un miembro por su ID
        public Miembro ObtenerPorId(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(@"
                    SELECT ID_Miembro, Nombre, Apellido, FechaNacimiento, Email, Telefono, ID_Membresia 
                    FROM Miembros 
                    WHERE ID_Miembro = @ID_Miembro", connection);

                command.Parameters.AddWithValue("@ID_Miembro", id);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Miembro
                    {
                        ID_Miembro = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        FechaNacimiento = reader.GetDateTime(3),
                        Email = reader.GetString(4),
                        Telefono = reader.GetString(5),
                        ID_Membresia = reader.GetInt32(6)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener miembro por ID: {ex.Message}");
            }
            return null;
        }

        // Actualizar un miembro existente
        public void Actualizar(Miembro miembro)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand(@"
                    UPDATE Miembros 
                    SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento,
                        Email = @Email, Telefono = @Telefono, ID_Membresia = @ID_Membresia
                    WHERE ID_Miembro = @ID_Miembro", connection);

                command.Parameters.AddWithValue("@ID_Miembro", miembro.ID_Miembro);
                command.Parameters.AddWithValue("@Nombre", miembro.Nombre);
                command.Parameters.AddWithValue("@Apellido", miembro.Apellido);
                command.Parameters.AddWithValue("@FechaNacimiento", miembro.FechaNacimiento);
                command.Parameters.AddWithValue("@Email", miembro.Email);
                command.Parameters.AddWithValue("@Telefono", miembro.Telefono);
                command.Parameters.AddWithValue("@ID_Membresia", miembro.ID_Membresia);

                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Miembro actualizado." : "No se encontró el miembro para actualizar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar miembro: {ex.Message}");
            }
        }

        // Eliminar un miembro por su ID
        public void Eliminar(int id)
        {
            try
            {
                using var connection = _dbManager.GetConnection();
                connection.Open();
                var command = new SqlCommand("DELETE FROM Miembros WHERE ID_Miembro = @ID_Miembro", connection);
                command.Parameters.AddWithValue("@ID_Miembro", id);
                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Miembro eliminado." : "No se encontró el miembro para eliminar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar miembro: {ex.Message}");
            }
        }
    }
}