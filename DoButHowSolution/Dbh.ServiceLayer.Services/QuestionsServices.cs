﻿using Dbh.BusinessLayer.Contracts;
using Dbh.Common.IoCContainer;
using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Interfaces;
using Dbh.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.ServiceLayer.Services
{
    public class QuestionServices: ServiceBase, IQuestionServices
    {

        public void ApproveQuestion(int questionId, string username)
        {
            var businessUoW = GetUoW();

            businessUoW.Questions.ApproveQuestion(questionId, username);

            businessUoW.SaveChanges();
        }
         
        public void RejectQuestion(int questionId, string rejectReason, string username)
        {
            var businessUoW = GetUoW();

            businessUoW.Questions.RejectQuestion(questionId, rejectReason, username);

            businessUoW.SaveChanges();
        }

        public void CorrectQuestion(int questionId, string title, string description)
        {
            var businessUoW = GetUoW();

            businessUoW.Questions.CorrectQuestion(questionId, title, description);

            businessUoW.SaveChanges();
        }

        public IEnumerable<Question> GetAll()
        {
            var businessUoW = GetUoW();
            
            return businessUoW.Questions.GetAll();
        }

        public IEnumerable<Question> GetApprovedQuestions()
        {
            var businessUoW = GetUoW();
            var questions = businessUoW.Questions.GetApprovedQuestions();
            return questions;
        }

        public IEnumerable<Question> GetNotApprovedQuestions()
        {
            var businessUoW = GetUoW();
            var questions = businessUoW.Questions.GetNotApprovedQuestions();
            return questions;
        }

        public Question GetQuestionById(int id)
        {
            var businessUoW = GetUoW();

            return businessUoW.Questions.GetQuestionById(id);
        }

        public bool CreateNewQuestion(Question question, ApplicationUser creator)
        {
            var businessUoW = GetUoW();

            businessUoW.Questions.CreateQuestion(question, creator);

            return businessUoW.SaveChanges() > 0;

        }

        public bool CreateNewQuestion(Question question, string creatorName)
        {
            var businessUoW = GetUoW();

            businessUoW.Questions.CreateQuestion(question, creatorName);

            return businessUoW.SaveChanges() > 0;

        }

        public IEnumerable<Question> GetQuestionsOfUser(string username)
        {
            var businessUoW = GetUoW();
            var questions = businessUoW.Questions.GetQuestionsOfUser(username);
            return questions;
        }

        public string GetQuestionTitle(int questionId)
        {
            var businessUoW = GetUoW();

            return businessUoW.Questions.GetQuestionTitle(questionId);
        }

        public IEnumerable<Question> GetAnsweredQuestions(int skip, int take)
        {
            var businessUoW = GetUoW();

            return businessUoW.Questions.GetAnsweredQuestions(skip, take);
        }

        public IEnumerable<QuestionCategory> GetQuestionCategories()
        {
            return GetUoW().Questions.GetQuestionCategories();
        }
    }
}
