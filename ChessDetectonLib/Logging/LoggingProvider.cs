using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;

namespace ChessDetectonLib.Logging
{
      internal static class LoggingProvider
      {
            /// <summary>
            /// 
            /// </summary>
            public readonly static ILogger Logger = new LoggerConfiguration()
                  .WriteTo
                  .File(@"ChessDetectionLibLog.log")
                  .MinimumLevel.Debug()
                  .CreateLogger();

            public static void HandleException(Exception exceptionToHandle, LogEventLevel level, string additionalText = "", bool throwException = true)
            {
                  Logger.Write(level, exceptionToHandle, additionalText);

                  if (throwException)
                        throw exceptionToHandle;
            }
      }
}
