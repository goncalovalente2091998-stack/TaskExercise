using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TaskExercise.Models;


namespace TaskExercise.Data
{
    public class Repository : IRepository
    {
        private readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=TaskManager;Trusted_Connection=True;TrustServerCertificate=True";

       
        public async Task<IEnumerable<ItemTask>> GetTasksAsync()
        {
            var tasks = new List<ItemTask>();

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("GetAllTasks", connection);
            command.CommandType = CommandType.StoredProcedure;

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tasks.Add(new ItemTask
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    IsCompleted = reader.GetBoolean(2),
                    CreatedAt = reader.GetDateTime(3)
                });
            }

            return tasks;
        }

        
        public async Task CreateTaskAsync(string title)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("CreateTask", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Title", title);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateTaskAsync(int id, bool isCompleted)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("UpdateTask", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@IsCompleted", isCompleted);

            await command.ExecuteNonQueryAsync();
        }
    }
}