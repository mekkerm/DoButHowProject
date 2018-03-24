using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public interface IAnswers
    {
        bool CreateAnswer(int questionId, string response, string creatorName);
        IEnumerable<Answer> GetNotApprovedAnswers(int skip, int take);
        Answer GetAnswerById(int id);
        void ApproveAnswer(int answerId, string username);
        void RejectAnswer(int answerId, string rejectReason, string username);
        IEnumerable<Answer> GetAnswers(int skip, int take);
        IEnumerable<Answer> GetAnswersOfQuestion(int questionId, string username);
        IEnumerable<Answer> GetAnswersOfUser(string username, int skip, int take);
        IEnumerable<Answer> GetRejectedAnswersOfUser(string username, int skip, int take);
        void CorrectAnswer(int answerId, string response);

        void AddOrModifyAnswerRating(int answerId, string username, decimal rating);

        void RemoveAnswerRating(int answerId, string username);

        RatingInformation GetRatingInformation(int asnwerId, string username);
    }
}
