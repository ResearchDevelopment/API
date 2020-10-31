namespace ShadiWebApplication.Logger
{
    using Newtonsoft.Json;
    using NLog;
    using System;


    public class NLogger : ILogger
    {
        private readonly NLog.Logger _logger;
        private readonly Guid _logID;

        private const string LogFormat = "Message: {message} | TraceID: {traceID} | Data: {data}";

        public NLogger(string loggerName, Guid? logID = null)
        {
            _logger = LogManager.GetLogger(loggerName);
            _logID = logID ?? Guid.NewGuid();
        }

        public void Info<TModel>(string message, TModel data)
        {
            try
            {
                string dataAsJson = "";

                if (data != null)
                {
                    if (data is string)
                    {
                        dataAsJson = data as string;
                    }
                    else
                    {
                        dataAsJson = JsonConvert.SerializeObject(data);
                    }

                }

                _logger.Info(LogFormat, arg1: message, arg2: _logID.ToString(), arg3: dataAsJson);
            }
            catch { }
        }

        public void Error(string message, Exception ex) =>
            _logger.Error
            (
                exception: ex,
                message: LogFormat, message, _logID.ToString(), ""
            );

        public Guid TraceLogID => this._logID;
    }
}
