using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Models.QuestionViewModels
{
    public class AllQuestionsViewModel
    {
        public AllQuestionsViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }

        public IList<QuestionViewModel> Questions;
    }
}
