using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dbh.Model.EF.Entities
{
    public class Question : BaseEntity
    {
        [MaxLength(80)]
        public string Title { get; set; }

        [MaxLength(100000)]
        public string Description { get; set; }

        public string CreatorId { get; set; }
        //[ForeignKey("CreatorUserId")]
        public ApplicationUser Creator { get; set; }

        public DateTime CreationDate { get; set; }
        public Boolean IsApproved { get; set; }

        public DateTime? ApproveDate { get; set; }

        public string ApproverId { get; set; }
        
        public DateTime? RejectDate { get; set; }
        public Boolean IsRejected { get; set; }

        public string RejectorId{ get; set; }

        public string RejectReason { get; set; }

        public Boolean? HasAnwser { get; set; }

        public int CategoryId { get; set; }
        [NotMapped]
        public string CategoryDescription { get; set; }
    }
}
