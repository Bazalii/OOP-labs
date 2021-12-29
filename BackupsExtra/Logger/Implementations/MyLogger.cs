using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;

namespace BackupsExtra.Logger.Implementations
{
    public class MyLogger
    {
        private Serilog.Core.Logger _logger;

        public MyLogger(Serilog.Core.Logger logger)
        {
            _logger = logger ??
                      throw new ArgumentNullException(
                          nameof(logger), "Logger cannot be null!");
        }

        public void Write(string information)
        {
            _logger.Information(information);
        }

        public void Write(List<string> logLines)
        {
            foreach (string logLine in logLines)
            {
                Write(logLine);
            }
        }

        public void SetLogger(Serilog.Core.Logger logger)
        {
            _logger = logger;
        }
    }
}