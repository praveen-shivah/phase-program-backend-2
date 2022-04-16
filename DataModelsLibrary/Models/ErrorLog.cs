namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class ErrorLog : BaseEntity
    {
        public string ClassName { get; set; }

        public string Hash { get; set; }

        public int LogClassId { get; set; }

        public string Message { get; set; }

        public string MethodName { get; set; }

        public string StackTrace { get; set; }
    }
}