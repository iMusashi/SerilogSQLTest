using Serilog.Core;
using Serilog.Events;

namespace SerilogSQLTest
{
    public static class LoggingLevel
    {
        public static LoggingLevelSwitch Switch { get; set; } = new LoggingLevelSwitch(LogEventLevel.Information);
    }
}
