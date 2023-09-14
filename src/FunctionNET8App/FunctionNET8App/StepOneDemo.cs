using FunctionNET8App.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace FunctionNET8App;

public class StepOneDemo
{
    private readonly ILogger _logger;

    public StepOneDemo(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<StepOneDemo>();
    }

    [Function("Function1")]
    public void Run(
        [RabbitMQTrigger("myqueue", ConnectionStringSetting = "Redis")] string myQueueItem,
        [SqlInput(commandText: "select [Id], [order], [title], [url], [completed] from dbo.ToDo where Id = @Id",
            commandType: System.Data.CommandType.Text,
            parameters: "@Id={Query.id}",
            connectionStringSetting: "SqlConnectionString")]
        IEnumerable<TodoItem> toDoItem)
    {
        _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
    }
}
