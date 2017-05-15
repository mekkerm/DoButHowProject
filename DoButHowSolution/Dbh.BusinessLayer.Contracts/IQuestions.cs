using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public interface IQuestions
    {
        void ApproveQuestion(int questionId, string username);
        void RejectQuestion(int questionId, string rejectReason, string username);
        void CorrectQuestion(int questionId, string title, string description);

        IEnumerable<Question> GetAll();

        Question GetQuestionById(int id);

        void CreateQuestion(Question question, ApplicationUser creator);


        void CreateQuestion(Question question, string creatorName);
        IEnumerable<Question> GetQuestionsOfUser(string username);
        IEnumerable<Question> GetNotApprovedQuestions();


        IEnumerable<Question> GetApprovedQuestions();
    }
}
