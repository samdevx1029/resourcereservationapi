using ResourceReservation.Abstractions;
using Serilog;
using ISerilogger = Serilog.ILogger;
using ILogger = ResourceReservation.Abstractions.ILogger;
using System;

namespace ResourceReservation.Logging
{
    public class Logger : ILogger
    {
        private readonly static Lazy<ILogger> lazy = new Lazy<ILogger>(()=>new Logger());

        private readonly ISerilogger _serilogger;

        private Logger()
        {
            _serilogger = new LoggerConfiguration()
                //.Enrich.WithThreadId()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
        }

        public static ILogger Instance
        {
            get { return lazy.Value; }
        }

        public void AppStarted()
        {
            throw new NotImplementedException();
        }

        public void LogDebug<T>(T details, string context, string message)
        {
            _serilogger.ForContext(context, details, true).Debug(message);
        }

        public void LogError<T>(T details, string context, string message)
        {
            _serilogger.ForContext(context, details, true).Error(message);
        }

        public void LogError<T>(Exception exception, T details, string context, string message)
        {
            _serilogger.ForContext(context, details, true).Error(exception, message);
        }

        public void LogFatal<T>(T details, string context, string message)
        {
            _serilogger.ForContext(context, details, true).Fatal(message);
        }

        public void LogInformation<T>(T details, string context, string message)
        {
            _serilogger.ForContext(context, details, true).Information(message);
        }

        public void LogVerbose<T>(T details, string context, string message)
        {
            _serilogger.ForContext(context, details, true).Verbose(message);
        }

        public void LogWarning<T>(T details, string context, string message)
        {
            _serilogger.ForContext(context, details, true).Warning(message);
        }

        public void RequestCompleted()
        {
            throw new NotImplementedException();
        }

        public void RequestStarted()
        {
            throw new NotImplementedException();
        }
    }
}
