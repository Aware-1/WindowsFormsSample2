using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.Elasticsearch;
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

            var elasticUri = new Uri("http://localhost:9200/");

            Log.Logger = new LoggerConfiguration().WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
               .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticUri)
               {
                   AutoRegisterTemplate = true,
                   IndexFormat = "logs-{0:yyyy.MM.dd}"
               }).CreateLogger();

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
