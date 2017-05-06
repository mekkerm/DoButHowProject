using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public interface IQuestions
    {
        void ApproveQuestion(Question question, ApplicationUser approver);

        IEnumerable<Question> GetAll();

        Question GetQuestionById(int id);

        void CreateQuestion(Question question, ApplicationUser creator);


        void CreateQuestion(Question question, string creatorName);
    }
}
