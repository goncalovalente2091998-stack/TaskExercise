using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using TaskExercise.Data;
using TaskExercise.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskExercise.Models;

namespace TaskExercise.Handlers
{
    public class TaskHandlers
    {
        private readonly IRepository _repository;

        public TaskHandlers(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<APIGatewayProxyResponse> HandleAsync(APIGatewayProxyRequest request)
        {
            if (request.HttpMethod == "GET")
            {
                var tasks = await _repository.GetTasksAsync();
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = JsonSerializer.Serialize(tasks),
                    Headers = new Dictionary<string, string> { ["Content-Type"] = "application/json" }
                };
            }

            if (request.HttpMethod == "POST")
            {
                var body = JsonSerializer.Deserialize<CreateTask>(request.Body);
                await _repository.CreateTaskAsync(body.Title);
                return new APIGatewayProxyResponse { StatusCode = 200, Body = "Task created" };
            }

            if (request.HttpMethod == "PUT")
            {
                var id = int.Parse(request.PathParameters["id"]);
                var body = JsonSerializer.Deserialize<Dto.UpdateTaskDto>(request.Body);
                await _repository.UpdateTaskAsync(id, body.IsCompleted);
                return new APIGatewayProxyResponse { StatusCode = 200, Body = "Task updated" };
            }


            return new APIGatewayProxyResponse { StatusCode = 405, Body = "Method Not Allowed" };
        }
      
    }


}