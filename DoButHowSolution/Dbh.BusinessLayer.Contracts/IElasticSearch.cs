using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public interface IElasticSearch
    {
        List<QuestionHeaderDTO> SearchForQuestion(string text);
    }
}
