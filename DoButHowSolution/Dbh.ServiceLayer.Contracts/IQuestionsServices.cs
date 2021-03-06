﻿using Dbh.BusinessLayer.Contracts;
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
        void ApproveQuestion(int questionId, string username);


        void RejectQuestion(int questionId, string rejectReason, string username);
        void CorrectQuestion(int questionId, string title, string description);

        IEnumerable<Question> GetQuestionsOfUser(string username, int take, int skip);

        /// <summary>
        /// Returns the list of questions which are need to be approved.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Question> GetNotApprovedQuestions(int take, int skip);

        IEnumerable<Question> GetApprovedQuestions(int take, int skip);

        IEnumerable<Question> GetAll();

        Question GetQuestionById(int id);
        string GetQuestionTitle(int questionId);
        IEnumerable<QuestionHeaderDTO> GetAnsweredQuestions(int skip, int take);

        IEnumerable<QuestionCategory> GetQuestionCategories();

        IEnumerable<QuestionHeaderDTO> FindQuestions(string text);
    }
}
