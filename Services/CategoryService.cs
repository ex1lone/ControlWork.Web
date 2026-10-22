using Microsoft.Data.Sqlite;
using ControlWork.Web.Models;

namespace ControlWork.Web.Services
{
    public class CategoryService
    {
        private readonly string _connectionString = "Data Source=catalog.db";

        public CategoryService()
        {
            EnsureTableCreated();
        }

        private void EnsureTableCreated()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Categories (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT
                );";
            command.ExecuteNonQuery();
        }

        public List<Category> GetAll()
        {
            var categories = new List<Category>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name, Description FROM Categories;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                });
            }

            return categories;
        }

        public void Add(Category category)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Categories (Name, Description) VALUES (@name, @description);";
            command.Parameters.AddWithValue("@name", category.Name);
            command.Parameters.AddWithValue("@description", category.Description ?? (object)DBNull.Value);

            command.ExecuteNonQuery();
        }
    }
}