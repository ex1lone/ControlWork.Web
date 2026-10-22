using Microsoft.Data.Sqlite;
using ControlWork.Web.Models;
using System.Collections.Generic;
using System;

namespace ControlWork.Web.Services
{
    public class ResourceService
    {
        // Поле класу (оголошене правильно, доступне всім методам нижче)
        private readonly string _connectionString = "Data Source=catalog.db";

        public ResourceService()
        {
            EnsureTablesCreated();
        }

        private void EnsureTablesCreated()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Resources (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    CategoryId INTEGER NOT NULL,
                    Name TEXT NOT NULL,
                    Sku TEXT NOT NULL,
                    Brand TEXT NOT NULL,
                    Price REAL NOT NULL,
                    Quantity INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Requests (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ResourceId INTEGER NOT NULL,
                    Quantity INTEGER NOT NULL,
                    RequestDate TEXT NOT NULL
                );";
            command.ExecuteNonQuery();
        }

        public List<Resource> GetAllResources()
        {
            var resources = new List<Resource>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, CategoryId, Name, Sku, Brand, Price, Quantity FROM Resources;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                resources.Add(new Resource
                {
                    Id = reader.GetInt32(0),
                    CategoryId = reader.GetInt32(1),
                    Name = reader.GetString(2),
                    Sku = reader.GetString(3),
                    Brand = reader.GetString(4),
                    Price = reader.GetDecimal(5),
                    Quantity = reader.GetInt32(6)
                });
            }

            return resources;
        }

        public bool CreateRequest(Request request)
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Requests (ResourceId, Quantity, RequestDate) 
                    VALUES (@resourceId, @quantity, @requestDate);";

                command.Parameters.AddWithValue("@resourceId", request.ResourceId);
                command.Parameters.AddWithValue("@quantity", request.Quantity);
                command.Parameters.AddWithValue("@requestDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}