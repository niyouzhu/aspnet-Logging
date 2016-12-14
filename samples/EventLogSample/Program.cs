using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Internal;

namespace EventLogSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var programCategoryName = typeof(Program).FullName; //nameof(Program);//
            var systemCategoryName = "System";
            var filterLoggerSettings = new FilterLoggerSettings //Set filters, must consistent with Logger name
            {
                {programCategoryName, LogLevel.Error},
                {systemCategoryName, LogLevel.Trace}
            };
            var factory = new LoggerFactory().WithFilter(filterLoggerSettings).AddEventLog().AddIConnectLog();
            var programLogger = factory.CreateLogger<Program>(); //this will use Program's full name to create name of Logger.
            var systemLogger = factory.CreateLogger(systemCategoryName);
            using (programLogger.BeginScope(new Program()))
            {
                programLogger.LogDebug("This is debug level"); // this level incompatible with windows EventLog 
                programLogger.LogInformation("This is infomation level");
                programLogger.LogCritical("This is critical level");
                programLogger.Log(LogLevel.Error, new EventId(10, "named Program"), new List<string> { { "Hello" }, { "World" } }, null,
                    (list, ex) =>
                    {
                        var message = string.Join(",", list);
                        if (ex != null)
                        {
                            message = message + ex;
                        }
                        return message;
                    });
            }

            using (systemLogger.BeginScope(new Program()))
            {
                systemLogger.LogDebug("This is debug level");
                systemLogger.LogCritical("This is critical lelve");
                systemLogger.LogWarning("This is warning level");
            }

        }

        public class ProgramEventSource : EventSource
        {
            
        }
    }
}
