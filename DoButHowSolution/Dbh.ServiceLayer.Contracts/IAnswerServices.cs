using Dbh.BusinessLayer.Contracts;
using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.ServiceLayer.Contracts
{
    public interface IAnswerServices
    {

        bool AnswerQuestion(int questionId, string answer, string currentUser);
        List<AnswerHeaderDTO> GetNotApprovedAnswers(int skip, int take);
        Answer GetAnswerById(int id);
        void ApproveAnswer(int answerId, string username);
        void RejectAnswer(int answerId, string rejectReason, string username);
        List<Answer> GetAnswers(int skip, int take);
        List<Answer> GetAnswersOfQuestion(int questionId, string username);
        List<AnswerHeaderDTO> GetAnswersOfUser(string username, int skip, int take);
        List<AnswerHeaderDTO> GetRejectedQuestionsOfUser(string username, int skip, int take);
        void CorrectAnswer(int answerId, string response);

        void AddOrModifyAnswerRating(int answerId, string username, decimal rating);
        void RemoveAnswerRating(int answerId, string username);

        RatingInformation GetRatingInformation(int asnwerId, string username);
    }
}
