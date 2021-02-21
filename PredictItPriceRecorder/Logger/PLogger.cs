using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;

namespace PredictItPriceRecorder.Logger
{
    public static class PLogger
    {
        private static readonly ILogger _logger;
        
        static PLogger()
        {
            _logger = new LoggerConfiguration()
                          .MinimumLevel.Debug()
                          .WriteTo.File(@"C:\Users\Josh\Documents\PredictIt\PredictProfitCalculator\Logs\log.txt", rollingInterval: RollingInterval.Day)
                          .CreateLogger();
        }

        public static void Info(string message)
        {
            try
            {
                _logger.Write(LogEventLevel.Information, message);
                //_logger.Write(LogEventLevel.Error, message);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public static void Error(string message)
        {
            try
            {
                _logger.Write(LogEventLevel.Error, message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}