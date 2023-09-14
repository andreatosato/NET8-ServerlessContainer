using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
     .ConfigureLogging(logging =>
     {
         var ruleApp = "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider";
         logging.Services.Configure<LoggerFilterOptions>(options =>
         {
             LoggerFilterRule? defaultRule = options.Rules.FirstOrDefault(rule => rule.ProviderName == ruleApp);
             if (defaultRule is not null)
             {
                 options.Rules.Remove(defaultRule);
             }
         });
     })

    .Build();

host.Run();
