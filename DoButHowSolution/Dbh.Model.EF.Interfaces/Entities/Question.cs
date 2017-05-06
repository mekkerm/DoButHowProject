using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dbh.Model.EF.Entities
{
    public class Question:BaseEntity
    {
        [MaxLength(80)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public string CreatorId { get; set; }
        //[ForeignKey("CreatorUserId")]
        public ApplicationUser Creator { get; set; }

        public DateTime CreationDate { get; set; }
        public Boolean IsApproved { get; set; }

        public DateTime ApproveDate { get; set; }

        public string ApproverId { get; set; }
        //[ForeignKey("ApproverUserId")]
        public ApplicationUser Approver { get; set; }

        //TODO add asnwers as navigation property

    }
}
