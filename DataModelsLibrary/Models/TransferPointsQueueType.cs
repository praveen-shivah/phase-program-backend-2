namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


    public partial class TransferPointsQueueType : BaseEntity
    {
        public TransferPointsQueueType()
        {
            TransferPointsQueue = new HashSet<TransferPointsQueue>();
        }

        [Key]
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? Name { get; set; }

        [InverseProperty("TransferPointsQueueType")]
        public virtual ICollection<TransferPointsQueue> TransferPointsQueue { get; set; }
    }
}
