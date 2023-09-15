using System.Text.Json;
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
    public async Task<DispatchedCachedItem> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        var items = await JsonSerializer.DeserializeAsync<IEnumerable<TodoItem>>(req.Body);

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