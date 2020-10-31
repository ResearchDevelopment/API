namespace ShadiWebApplication.Logger
{
    using System;

    public interface ILogger
    {
        void Info<TModel>(string message, TModel data);
        void Error(string message, Exception ex);
        Guid TraceLogID { get; }
    }
}
