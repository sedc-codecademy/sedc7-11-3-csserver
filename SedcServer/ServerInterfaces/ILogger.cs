using System;

namespace ServerInterfaces
{
    public interface ILogger
    {
        LogLevel Level { get; }

        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message, Exception exception = null);
        void Fatal(string message, Exception exception = null);

        void LogMessage(LogLevel level, string message, Exception exception = null);
    }

    public enum LogLevel
    {
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4,
        Fatal = 5
    }
}
