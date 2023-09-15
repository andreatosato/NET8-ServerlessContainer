using FunctionNET8App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

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
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
        FunctionContext context,
        [FromBody] IEnumerable<TodoItem> items)
    {
        //var body = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TodoItem>>(context.GetHttpContext().Request.Body);

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
    public IActionResult HttpResponse { get; set; } = new OkResult();
}