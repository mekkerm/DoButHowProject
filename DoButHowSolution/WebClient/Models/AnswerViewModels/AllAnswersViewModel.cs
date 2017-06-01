using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Models.AnswerViewModels
{
    public class AllAnswersViewModel
    {
        public AllAnswersViewModel()
        {
            AnswerList = new List<AnswerViewModel>();
        }

        public IList<AnswerViewModel> AnswerList;
    }
}
