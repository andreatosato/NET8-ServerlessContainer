//using FunctionNET8App.Models;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Azure.Functions.Worker.Extensions.Sql;
//using Microsoft.Extensions.Logging;

//namespace FunctionNET8App;

//public class StepTwoDemo
//{
//    private readonly ILogger _logger;

//    public StepTwoDemo(ILoggerFactory loggerFactory)
//    {
//        _logger = loggerFactory.CreateLogger<StepTwoDemo>();
//    }

//    [Function("StepTwoDemo")]
//    public async Task Run(
//        [SqlTrigger("dbo.", "Demo")] TodoItem item)
//    {
//        _logger.LogInformation("C# HTTP trigger function processed a request.");

//        //var response = req.CreateResponse(HttpStatusCode.OK);
//        //response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

//        //response.WriteString("Welcome to Azure Functions!");

//        //return response;
//    }
//}
