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

        [StringLength(250)]
        public string Description { get; set; }

        public string CreatorName { get; set; }

        public string CreatorId { get; set; }

        public int QuestionId { get; set; }
    }
}
