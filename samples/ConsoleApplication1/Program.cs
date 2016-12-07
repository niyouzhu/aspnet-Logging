using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSourceSamples;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var formatString = "prefix{0:yyyy/MM/dd hh:mm:ss} ---{1:N2}----{2:X}----suffix";
            //var formatter = new LogValuesFormatter(formatString);
            //var objects = new object[] { DateTime.Now, 2.4, 14 };
            //var str1 = formatter.Format(objects);

            //var formattedLogValues = new FormattedLogValues(formatString, objects);
            //var loggerFactory = new LoggerFactory();
            //loggerFactory.AddConsole();
            //var logger = loggerFactory.CreateLogger<SerializableAttribute>();
            //logger.BeginScope<AccessViolationException>(new AccessViolationException() { });
            //logger.Log<NotImplementedException>(LogLevel.Information, new EventId(), new NotImplementedException(),
            //    new FieldAccessException("this"), (not, field) =>
            //    {
            //        return not.ToString() + field.ToString();
            //    });
            //Console.Read();
            AllSamples.Run();
        }
    }
}
