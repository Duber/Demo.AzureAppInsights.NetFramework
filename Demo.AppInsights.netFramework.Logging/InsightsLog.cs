using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DataContracts;

namespace Demo.AppInsights.netFramework.Logging
{
    public interface Log
    {
        void Debug(string message);
        void Error(Exception ex);
        void Info(string message);
        void Warn(string message);
    }

    public class InsightsLog : Log
    {
        private readonly TelemetryClient _appInsightsClient;
        private LogLevel _logLevel = LogLevel.Debug; // ToDo: read from config

        public InsightsLog(Type loggedClass, TelemetryClient telemetryClient)
        {
            _appInsightsClient = telemetryClient;
        }

        public void Debug(string message)
        {
            if (_logLevel <= LogLevel.Debug)
            {
                _appInsightsClient.TrackTrace(message, SeverityLevel.Verbose);
            }
        }

        public void Error(Exception ex)
        {
            if (_logLevel <= LogLevel.Error)
            {
                _appInsightsClient.TrackException(ex);
                _appInsightsClient.Flush();
            }
        }

        public void Info(string message)
        {
            if (_logLevel <= LogLevel.Info)
            {
                _appInsightsClient.TrackTrace(message, SeverityLevel.Information);
            }
        }

        public void Warn(string message)
        {
            if (_logLevel <= LogLevel.Warn)
            {
                _appInsightsClient.TrackTrace(message, SeverityLevel.Warning);
            }
        }
    }

    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error
    }
}
