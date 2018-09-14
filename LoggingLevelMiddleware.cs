using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog.Events;
using System.Threading.Tasks;

namespace SerilogSQLTest
{
    public class LoggingLevelMiddleware
    {
        private readonly RequestDelegate _next;
        private LoggingLevelSwitchOptions LoggingLevelOptionsUno;
        private LoggingLevelSwitchOptions LoggingLevelOptionsDos;
        private LoggingLevelSwitchOptions LoggingLevelOptionsTres;
        public IConfiguration Configuration { get; }
        public LoggingLevelMiddleware(RequestDelegate next
            ,IConfiguration configuration)
        {
            _next = next;
            Configuration = configuration;
        }

        public Task Invoke(HttpContext context, IOptionsMonitor<LoggingLevelSwitchOptions> optionsAccessorUno,
            IOptions<LoggingLevelSwitchOptions> optionsAccessorDos, IOptionsSnapshot<LoggingLevelSwitchOptions> optionsAccessorTres)
        {
            LoggingLevelOptionsUno = optionsAccessorUno.CurrentValue;
            LoggingLevelOptionsDos = optionsAccessorDos.Value;
            LoggingLevelOptionsTres = optionsAccessorTres.Value;

            var x = context.RequestServices.GetRequiredService<IOptionsSnapshot<LoggingLevelSwitchOptions>>().Value;
            switch (LoggingLevelOptionsUno.SerilogLoggingLevel)
            {
                case "Warning":
                    LoggingLevel.Switch.MinimumLevel = LogEventLevel.Warning;
                    break;
                case "Error":
                    LoggingLevel.Switch.MinimumLevel = LogEventLevel.Error;
                    break;
                case "Fatal":
                    LoggingLevel.Switch.MinimumLevel = LogEventLevel.Fatal;
                    break;
                case "Information":
                default:
                    LoggingLevel.Switch.MinimumLevel = LogEventLevel.Information;
                    break;

            }

            return _next(context);
        }
    }
}
