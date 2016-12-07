using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Internal;

namespace EventSourceSample
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var programCategoryName = typeof(Program).FullName;

            var filterLoggerSettings = new FilterLoggerSettings //Set filters, must consistent with Logger name
            {
                {programCategoryName, LogLevel.Error},
            };
            var factory = new LoggerFactory().WithFilter(filterLoggerSettings).AddEventSourceLogger();
            var programLogger = factory.CreateLogger<Program>(); //this will use Program's full name to create name of Logger.
            using (programLogger.BeginScope(new Program()))
            {
                using (var listener = new ConsoleListener())
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


            }



            Console.Read();

        }



        public class ConsoleListener : EventListener
        {

            protected override void OnEventSourceCreated(EventSource eventSource)
            {
                EnableEvents(eventSource, EventLevel.LogAlways);
                base.OnEventSourceCreated(eventSource);
            }

            private static readonly TextWriter Out = Console.Out;

            protected override void OnEventWritten(EventWrittenEventArgs eventData)
            {
                var message = $"Message: {eventData.Message}, Payload: {string.Join(",", eventData.Payload)}";
                Out.WriteLine(message);
            }
        }



    }


}
