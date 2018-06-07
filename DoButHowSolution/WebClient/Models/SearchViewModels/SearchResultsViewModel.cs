using Dbh.BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Models.SearchViewModels
{
    public class SearchResultsViewModel
    {
        public SearchResultsViewModel()
        {
            Hits = new List<QuestionHeaderDTO>();
        }
        public double QueryDuration { get; set; }

        public List<QuestionHeaderDTO> Hits { get; set; }
    }
}
