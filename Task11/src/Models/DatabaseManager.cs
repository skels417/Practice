using System;
using System.Data.SqlClient;

namespace FortuneTellerApp
{
    public class DatabaseManager
    {
        public static void DropDatabase(string connectionString, string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"DROP DATABASE IF EXISTS {databaseName};", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}