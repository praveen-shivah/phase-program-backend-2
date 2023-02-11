namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(Hash), nameof(CreatedOn), Name = "IX_ErrorLog_Hash")]
    public partial class ErrorLog : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string ClassName { get; set; } = null!;
        public string Hash { get; set; } = null!;
        public int LogClassId { get; set; }
        public string Message { get; set; } = null!;
        public string MethodName { get; set; } = null!;
        public string StackTrace { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
