using System;

namespace ResourceReservation.Abstractions
{
    public interface ILogger
    {
        void AppStarted();
        void RequestStarted();
        void RequestCompleted();

        void LogInformation<T>(T details, string context, string message);
        void LogWarning<T>(T details, string context, string message);
        void LogError<T>(T details, string context, string message);
        void LogError<T>(Exception exception, T details, string context, string message);
        void LogDebug<T>(T details, string context, string message);
        void LogVerbose<T>(T details, string context, string message);
        void LogFatal<T>(T details, string context, string message);
    }
}
