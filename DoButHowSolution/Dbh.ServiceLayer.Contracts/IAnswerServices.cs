using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.ServiceLayer.Contracts
{
    public interface IAnswerServices
    {

        bool AnswerQuestion(int questionId, string answer, string currentUser);
        List<Answer> GetNotApprovedAnswers();
    }
}
