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
                MessageBox.Show($"Miembro creado con ID: {miembro.ID_Miembro}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear miembro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Incluir el campo ID_Membresia en la consulta
                var command = new SqlCommand("SELECT ID_Miembro, Nombre, Apellido, FechaNacimiento, Email, Telefono, ID_Membresia FROM Miembros", connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Asignar todos los valores, incluyendo ID_Membresia
                    miembros.Add(new Miembro
                    {
                        ID_Miembro = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        FechaNacimiento = reader.GetDateTime(3),
                        Email = reader.GetString(4),
                        Telefono = reader.GetString(5),
                        ID_Membresia = reader.IsDBNull(6) ? 0 : reader.GetInt32(6) // Validar si es nulo
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener miembros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Error al obtener miembro por ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(rows > 0 ? "Miembro actualizado." : "No se encontró el miembro para actualizar.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar miembro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Ejecutar la consulta
                int rows = command.ExecuteNonQuery();

                // Mostrar mensaje dependiendo del resultado
                if (rows > 0)
                {
                    MessageBox.Show("Miembro eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró el miembro para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex) when (ex.Number == 547) // Código de error de restricción de clave foránea
            {
                // Manejar el error de clave foránea
                MessageBox.Show("No se puede eliminar el miembro porque tiene reservas asociadas. Elimine las reservas primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro error
                MessageBox.Show($"Error al eliminar miembro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}