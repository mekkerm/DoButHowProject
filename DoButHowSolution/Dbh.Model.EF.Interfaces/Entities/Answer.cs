using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dbh.Model.EF.Entities
{
    public class Answer : BaseEntity
    {
        public int QuestionId { get; set; }

        [MaxLength(1500)]
        public string Response { get; set; }

        public string CreatorId { get; set; }
        
        public ApplicationUser Creator { get; set; }

        public DateTime CreationDate { get; set; }

        public Boolean IsApproved { get; set; }

        public DateTime? ApproveDate { get; set; }

        public string ApproverId { get; set; }

        public DateTime? RejectDate { get; set; }

        public Boolean IsRejected { get; set; }

        public string RejectorId { get; set; }

        public string RejectReason { get; set; }

        [NotMapped]
        public decimal AverageRating { get; set; }

        [NotMapped]
        public decimal CurrentUserRating { get; set; }
        [NotMapped]
        public decimal CurrentRatingCount { get; set; }
    }
}
