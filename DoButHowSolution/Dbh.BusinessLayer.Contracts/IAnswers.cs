using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public interface IAnswers
    {
        bool CreateAnswer(int questionId, string response, string creatorName);
        IEnumerable<Answer> GetNotApprovedAnswers();
        Answer GetAnswerById(int id);
        void ApproveAnswer(int answerId, string username);
        void RejectAnswer(int answerId, string rejectReason, string username);
    }
}
