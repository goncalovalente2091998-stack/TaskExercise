using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using TaskExercise.Data;
using TaskExercise.Handlers;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TaskExercise
{
    public class TaskFunctions
    {
        private readonly TaskHandlers _handler;

        public TaskFunctions()
        {
            // Cria o repository
            var repository = new Repository();

            // Cria o handler passando o repository
            _handler = new TaskHandlers(repository);
        }

        public Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            return _handler.HandleAsync(request);
        }
    }
}