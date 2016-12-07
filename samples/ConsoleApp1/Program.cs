using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Filter;
using Microsoft.Extensions.Logging.Internal;
using Microsoft.Extensions.Logging.Console;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var formatString = "prefix{0:yyyy/MM/dd hh:mm:ss} ---{1:N2}----{2:X}----suffix";
            var formatter = new LogValuesFormatter(formatString);
            var objects = new object[] { DateTime.Now, 2.4, 14 };
            var str1 = formatter.Format(objects);

            var formattedLogValues = new FormattedLogValues(formatString, objects);
            var filterLoggerSettgins = new FilterLoggerSettings();
            filterLoggerSettgins.Add(nameof(NotImplementedException), LogLevel.Error);
            filterLoggerSettgins.Add(nameof(DateTime), LogLevel.Critical);
            var loggerFactory = new LoggerFactory().WithFilter(filterLoggerSettgins);
            var logger = loggerFactory.CreateLogger<FormatException>();
            using (logger.BeginScope<MemberAccessException>(new MemberAccessException() { }))
            {
                logger.Log<NotImplementedException>(LogLevel.Information, new EventId(), new NotImplementedException(),
                     new FieldAccessException("this"), (not, field) =>
                     {
                         return not.ToString() + "whatuwnat" + field.ToString();
                     });
                logger.LogInformation(new EventId(), new OutOfMemoryException(), "abcdefghijklmn{0:yyyy/MM/dd}",
                    new[] { DateTime.Now });
                using (logger.BeginScope("{0:yyyy/MM/dd}", new[] { DateTime.Now }))
                {
                    logger.Log<DateTime>(LogLevel.Critical, new EventId(), DateTime.Now, null, (dt, e) =>
                    {
                        return string.Format("{0:yyyy/dd}", dt);
                    });
                }

            }

            Console.Read();
        }
    }
}
