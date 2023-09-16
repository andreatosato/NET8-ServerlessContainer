using FunctionNET8App.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionNET8App;

public class LogToRabbit
{
    private readonly ILogger _logger;

    public LogToRabbit(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<LogToRabbit>();
    }

    [Function("LogToRabbit")]
    public void Run([RabbitMQTrigger("Todo", ConnectionStringSetting = "RabbitConnection")] TodoItem messageItem)
    {
        _logger.LogInformation($"Element exist so we log to Rabbit: {messageItem.Id}, {messageItem.Note}");
    }
}
