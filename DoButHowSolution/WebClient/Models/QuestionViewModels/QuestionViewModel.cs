using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebClient.Models.AnswerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Models.QuestionViewModels
{
    public class QuestionViewModel
    {
        [StringLength(80)]
        public string Title { get; set; }
        
        public string Description { get; set; }

        public string CreatorName { get; set; }

        public string CreatorId { get; set; }

        public int QuestionId { get; set; }

        public string RejectReason { get; set; }

        public bool IsRejected { get; set; }

        public bool IsApproved { get; set; }

        public string Status { get; set; }
        public bool CurrentUserIsTheOwner { get; set; }
        public bool DisableInputs { get; set; }

        public SelectList QuestionCategories { get; set; }

        public int QuestionCategoryId { get; set; }
        public string QuestionCategoryDescription { get; set; }
    }

    public class QuestionFullModel : QuestionViewModel
    {
        public IList<AnswerViewModel> AnswerList;
    }
}
