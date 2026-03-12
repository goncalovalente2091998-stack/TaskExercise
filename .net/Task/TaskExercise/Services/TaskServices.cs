using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using TaskExercise.Handlers;
using TaskExercise.Models;

namespace TaskExercise.Services
{
    public class TaskServices
    {
        private readonly TaskHandlers _handler;

        public TaskServices(TaskHandlers handler)
        {
            _handler = handler;
        }

        public async Task<IEnumerable<Dto.FrontendTaskDto>> GetTasksAsync()
        {
            var request = new APIGatewayProxyRequest { HttpMethod = "GET" };
            var response = await _handler.HandleAsync(request);

            var tasks = JsonSerializer.Deserialize<List<ItemTask>>(response.Body);

            return tasks.Select(t => new Dto.FrontendTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.IsCompleted ? "done" : "pending",
                CreatedAt = t.CreatedAt
            });
        }

        public async Task CreateTaskAsync(string title)
        {
            var request = new APIGatewayProxyRequest
            {
                HttpMethod = "POST",
                Body = JsonSerializer.Serialize(new { Title = title })
            };
            await _handler.HandleAsync(request);
        }
        public async Task UpdateTaskAsync(int id, bool isCompleted)
        {
            var request = new APIGatewayProxyRequest
            {
                HttpMethod = "PUT",
                PathParameters = new Dictionary<string, string>
                {
                    ["id"] = id.ToString()
                },
                Body = JsonSerializer.Serialize(new { IsCompleted = isCompleted })
            };

            await _handler.HandleAsync(request);
        }
    }

}