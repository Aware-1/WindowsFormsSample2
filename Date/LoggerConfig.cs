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
        public ILogger _logger;

        public LoggerConfig()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            _logger = Log.Logger;
        }

        public void LogError(string error, string name = null)
        {
            _logger.Error("{Error} {name}", error, name);
        }
        public void Information(string error, string name = null)
        {
            _logger.Information("{Error} {name}", error, name);
        }
        public void LogWarning(string error, string name = null)
        {
            _logger.Warning("{Error} {name}", error, name);
        }
    }
}
