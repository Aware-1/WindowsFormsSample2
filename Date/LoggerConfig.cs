using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class LoggerConfig
    {
        private ILogger _logger;

        public LoggerConfig()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            _logger = Log.Logger;
        }

        public void LogError(string error)
        {
            _logger.Error(error);
        }
        public void Information(string x)
        {
            _logger.Information(x);
        }
        public void LogWarning(string x)
        {
            _logger.Warning(x);
        }
    }
}
