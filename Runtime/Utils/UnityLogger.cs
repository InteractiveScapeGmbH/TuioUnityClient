using System;
using Microsoft.Extensions.Logging;

namespace TuioUnity.Utils
{
    public class UnityLogger : ILogger
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Information:
					
                    UnityEngine.Debug.Log(FormatMessage(state, exception, formatter));
                    break;

                case LogLevel.Warning:
                case LogLevel.Critical:
                    UnityEngine.Debug.LogWarning(FormatMessage(state, exception, formatter));
                    break;

                case LogLevel.Error:
                    if (exception != null)
                    {
                        UnityEngine.Debug.LogException(exception);
                    }
                    else
                    {
                        UnityEngine.Debug.LogError(FormatMessage(state, exception, formatter));
                    }
                    break;

                case LogLevel.None:
                    break;

                default:
                    break;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }
        
        private object FormatMessage<TState>(TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string now = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ss");
            string message = formatter.Invoke(state, exception);
            return $"{now}: {message}";
        }
    }
}