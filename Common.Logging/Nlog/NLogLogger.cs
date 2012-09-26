using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Logging.NLog
{
    public class NLogLogger : ILogger
    {
       private Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(LogUtil.BuildExceptionMessage(x));
        }

        public void Error(Exception x, string message)
        {
            Error(String.Format("Message: {0} | Error: {1}", message, LogUtil.BuildExceptionMessage(x)));
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtil.BuildExceptionMessage(x));
        }

        public void Fatal(Exception x, string message)
        {
            Fatal(String.Format("Message: {0} | Error: {1}", message, LogUtil.BuildExceptionMessage(x)));
        }
    }
}
