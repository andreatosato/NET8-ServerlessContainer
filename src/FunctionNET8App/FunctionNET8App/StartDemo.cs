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
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        return new OkResult();
    }
}
