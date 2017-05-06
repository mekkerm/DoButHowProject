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
        public String Title;

        [StringLength(250)]
        public String Description;

        public String CreatorName;

        public String CreatorId;

        public int QuestionId;
    }
}
