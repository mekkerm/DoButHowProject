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
        Answer GetAnswerById(int id);
        void ApproveAnswer(int answerId, string username);
        void RejectAnswer(int answerId, string rejectReason, string username);
        List<Answer> GetAnswers(int skip, int take);
        List<Answer> GetAnswersOfQuestion(int questionId);
        List<Answer> GetAnswersOfUser(string username);
        List<Answer> GetRejectedQuestionsOfUser(string username);
        void CorrectAnswer(int answerId, string response);
    }
}
