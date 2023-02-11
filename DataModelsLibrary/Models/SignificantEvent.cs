namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(Id), nameof(ShortDescription), nameof(CreatedBy), nameof(EventTypeId), nameof(CreatedOn), Name = "IX_SignificantEvent_EventId_CreatedOn")]
    public partial class SignificantEvent : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int EventTypeId { get; set; }
        public string LongDescription { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
