﻿using Dbh.BusinessLayer.Contracts;
using Dbh.Common.IoCContainer;
using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Utility;
using Dbh.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dbh.ServiceLayer.Services
{
    public class AnswerServices :ServiceBase, IAnswerServices
    {

        public bool AnswerQuestion(int questionId, string answer, string currentUser)
        {
            var businessUoW = GetBusinessObjectFactory();
            try
            {
                var result = businessUoW.Answers.CreateAnswer(questionId, answer, currentUser);

            }catch(BusinessException ex)
            {
                throw new ServiceException(ex);
            }

            return businessUoW.SaveChanges() > 0;
        }

        public List<AnswerHeaderDTO> GetNotApprovedAnswers(int skip, int take)
        {
            var businessUoW = GetBusinessObjectFactory();

            return businessUoW.Answers.GetNotApprovedAnswers(skip, take).ToList();
        }

        public Answer GetAnswerById(int id)
        {
            var businessUoW = GetBusinessObjectFactory();

            return businessUoW.Answers.GetAnswerById(id);
        }

        public void ApproveAnswer(int answerId, string username)
        {
            var businessUoW = GetBusinessObjectFactory();

            businessUoW.Answers.ApproveAnswer(answerId, username);

            businessUoW.SaveChanges();
        }

        public void RejectAnswer(int answerId, string rejectReason, string username)
        {
            var businessUoW = GetBusinessObjectFactory();

            businessUoW.Answers.RejectAnswer(answerId, rejectReason, username);

            businessUoW.SaveChanges();
        }

        public List<Answer> GetAnswers(int skip, int take)
        {
            var businessUoW = GetBusinessObjectFactory();

            return businessUoW.Answers.GetAnswers(skip, take).ToList();
        }

        public List<Answer> GetAnswersOfQuestion(int questionId, string username)
        {
            var businessUoW = GetBusinessObjectFactory();

            return businessUoW.Answers.GetAnswersOfQuestion(questionId, username).ToList();
        }

        public List<AnswerHeaderDTO> GetAnswersOfUser(string username, int skip, int take)
        {
            var businessUoW = GetBusinessObjectFactory();

            return businessUoW.Answers.GetAnswersOfUser(username, skip, take).ToList();
        }

        public List<AnswerHeaderDTO> GetRejectedQuestionsOfUser(string username, int skip, int take)
        {
            var businessUoW = GetBusinessObjectFactory();

            return businessUoW.Answers.GetRejectedAnswersOfUser(username, skip, take).ToList();
        }

        public void CorrectAnswer(int answerId, string response)
        {
            var businessUoW = GetBusinessObjectFactory();

            businessUoW.Answers.CorrectAnswer(answerId, response);

            businessUoW.SaveChanges();
        }

        public void AddOrModifyAnswerRating(int answerId, string username, decimal rating)
        {

            var businessUoW = GetBusinessObjectFactory();

            businessUoW.Answers.AddOrModifyAnswerRating(answerId, username, rating);

            businessUoW.SaveChanges();
        }
        public void RemoveAnswerRating(int answerId, string username)
        {

            var businessUoW = GetBusinessObjectFactory();

            businessUoW.Answers.RemoveAnswerRating(answerId, username);

            businessUoW.SaveChanges();
        }

        public RatingInformation GetRatingInformation(int asnwerId, string username)
        {
            var buow = GetBusinessObjectFactory();

            return buow.Answers.GetRatingInformation(asnwerId, username);
        }
    }
}
