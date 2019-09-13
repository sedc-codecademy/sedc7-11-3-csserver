using System;
using System.Collections.Generic;
using System.Text;
using ServerInterfaces;

namespace ServerCore.Logger
{
    internal class ConsoleLogger : ILogger
    {
        public ConsoleLogger(LogLevel level = LogLevel.Debug)
        {
            Level = level;
        }

        public LogLevel Level { get; }

        public void Debug(string message) => LogMessage(LogLevel.Debug, message);

        public void Info(string message) => LogMessage(LogLevel.Info, message);

        public void Warning(string message) => LogMessage(LogLevel.Warning, message);

        public void Error(string message, Exception exception = null) => LogMessage(LogLevel.Error, message, exception);

        public void Fatal(string message, Exception exception = null) => LogMessage(LogLevel.Fatal, message, exception);

        public void LogMessage(LogLevel level, string message, Exception exception = null)
        {
            if (this.Level > level)
            {
                return;
            }
            Console.WriteLine(message);
            if (exception != null)
            {
                Console.WriteLine(exception);
            }
        }

    }
}
