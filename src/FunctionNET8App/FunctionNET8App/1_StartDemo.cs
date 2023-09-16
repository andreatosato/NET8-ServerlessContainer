using FunctionNET8App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace FunctionNET8App;

public class StartDemo
{
    private readonly ILogger _logger;

    public StartDemo(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<StartDemo>();
    }

    [Function("StartDemo")]
    public DispatchedCachedItem RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "startDemo/{id}")] HttpRequest req,
        FunctionContext context,
        [SqlInput(commandText: "select [Id], [DueDate], [ToDo], [Note] from dbo.ToDo where Id = @Id",
            commandType: System.Data.CommandType.Text,
            parameters: "@Id={id}",
            connectionStringSetting: "SqlConnectionString")] IEnumerable<TodoItem> dbItem,
        [FromBody] IEnumerable<TodoItem> items)
    {

        // If not exist in DB, insert
        if (dbItem == null || !dbItem.Any())
            return new DispatchedCachedItem
            {
                HttpResponse = new OkResult(),
                WriteItem = items.First()!
            };

        // Else, send to Rabbit and log it
        return new DispatchedCachedItem
        {
            HttpResponse = new OkResult(),
            Items = items!
        };
    }
}


public class DispatchedCachedItem
{
    [RabbitMQOutput(QueueName = "Todo", ConnectionStringSetting = "RabbitConnection")]
    public IEnumerable<TodoItem> Items { get; set; } = Enumerable.Empty<TodoItem>();

    [SqlOutput(commandText: "dbo.Todo", connectionStringSetting: "SqlConnectionString")]
    public TodoItem? WriteItem { get; set; }

    public IActionResult HttpResponse { get; set; } = new OkResult();
}