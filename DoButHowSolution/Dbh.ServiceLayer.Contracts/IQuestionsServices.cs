using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.ServiceLayer.Contracts
{
    public interface IQuestionServices
    {
        bool CreateNewQuestion(Question question, ApplicationUser creator);
        
        bool CreateNewQuestion(Question question, string creatorName);
        void ApproveQuestion(Question question, ApplicationUser approver);

        IEnumerable<Question> GetQuestionsOfUser(string userId);

        IEnumerable<Question> GetNotApprovedQuestions();

        IEnumerable<Question> GetApprovedQuestions();

        IEnumerable<Question> GetAll();

        Question GetQuestionById(int id);

    }
}
