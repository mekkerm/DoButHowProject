using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Models.AnswerViewModels
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public string Response { get; set; }
        public string CreatorName { get; set; }

        public string CreatorId { get; set; }

        public int QuestionId { get; set; }

        public string RejectReason { get; set; }

        public bool IsRejected { get; set; }

        public bool IsApproved { get; set; }

        public string Status { get; set; }

        public bool DisableInputs { get; set; }

        public string QuestionTitle { get; set; }
        
        public bool CurrentUserIsTheOwner { get; set; }
    }
}
