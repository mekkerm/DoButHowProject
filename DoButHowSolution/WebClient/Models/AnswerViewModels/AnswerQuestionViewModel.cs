using MVCWebClient.Models.QuestionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Models.AnswerViewModels
{
    public class AnswerQuestionViewModel
    {
        public AnswerViewModel Answer { get; set; }

        public QuestionViewModel Question { get; set; }
        public bool DisableInputs { get; set; }
    }
}
