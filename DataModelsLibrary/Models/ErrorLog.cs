namespace DataModelsLibrary
{
    using System.ComponentModel.DataAnnotations;

    using DataSharedLibrary;

    public class ErrorLog : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public int LogClassId { get; set; }
        
        [MaxLength(100)]
        public string ClassName { get; set; }

        [MaxLength(100)]
        public string MethodName { get; set; }
        
        public string Message { get; set; }
        public string StackTrace { get; set; }

        [MaxLength(32)]
        public string Hash { get; set; }
    }
}
