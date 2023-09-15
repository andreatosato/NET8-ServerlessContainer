using FunctionNET8App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
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

    [Function("StartDemoCache")]
    public DispatchedCachedItem RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
        FunctionContext context,
        [FromBody] IEnumerable<TodoItem> items)
    {
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