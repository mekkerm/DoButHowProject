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
        IEnumerable<Question> GetQuestionsOfUser(string username, int take, int skip);
        IEnumerable<Question> GetNotApprovedQuestions(int take, int skip);


        IEnumerable<Question> GetApprovedQuestions(int take, int skip);
        string GetQuestionTitle(int questionId);
        IEnumerable<QuestionHeaderDTO> GetAnsweredQuestions(int skip, int take);

        IEnumerable<QuestionCategory> GetQuestionCategories();

        IEnumerable<QuestionHeaderDTO> FindQuestions(string text);
    }
}
