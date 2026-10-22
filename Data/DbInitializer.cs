using Microsoft.Data.Sqlite;

namespace ControlWork.Web.Data
{
    public class DbInitializer
    {
        private readonly string _connectionString = "Data Source=catalog.db";

        public void Initialize()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Categories (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT
                );

                CREATE TABLE IF NOT EXISTS Resources (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    CategoryId INTEGER,
                    Details TEXT,
                    Price DECIMAL
                );

                CREATE TABLE IF NOT EXISTS Requests (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ResourceId INTEGER,
                    CustomerName TEXT NOT NULL,
                    Status TEXT,
                    CreatedAt TEXT
                );
            ";
            command.ExecuteNonQuery();
        }
    }
}