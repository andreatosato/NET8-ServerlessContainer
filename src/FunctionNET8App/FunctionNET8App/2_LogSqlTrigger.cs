using FunctionNET8App.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace FunctionNET8App;

public class StepTwoDemo
{
    private readonly ILogger _logger;

    public StepTwoDemo(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<StepTwoDemo>();
    }

    [Function("LogSqlTrigger")]
    public async Task Run([SqlTrigger("dbo.ToDo", connectionStringSetting: "SqlConnectionString")]
        IReadOnlyList<SqlChange<TodoItem>> item)
    {
        var element = item.First();
        _logger.LogInformation($"Element created in DB {element.Item.Id}, {element.Item.ToDo}");
    }
}
