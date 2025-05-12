using System;
using System.Windows.Forms; // Agregué esta referencia para usar MessageBox
using Microsoft.Data.SqlClient;

namespace GimnasioManager.Utils
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager()
        {
            _connectionString = @"Data Source=LAPTOP-TTSBVU8R\SQLEXPRESS;Initial Catalog=GimnasioDB;Integrated Security=True;TrustServerCertificate=True";
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }

        public bool TestConnection()
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}