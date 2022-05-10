namespace LoggingServicesLibrary
{
    using System;

    using LoggingLibrary;

    public class ErrorLogDbPostingRequest
    {
        public ErrorLogDbPostingRequest(string className,
                                        LogClass logClass,
                                        string message,
                                        string methodName,
                                        string stackTrace)
        {
            this.ClassName = className;
            this.LogClass = logClass;
            this.Message = message;
            this.MethodName = methodName;
            this.StackTrace = stackTrace;
        }

        public string ClassName { get; }
        public LogClass LogClass { get; }
        public string Message { get; }
        public string MethodName { get; }
        public string StackTrace { get; }
    }
}