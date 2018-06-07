using Dbh.BusinessLayer.Contracts;
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
            var bof = GetBusinessObjectFactory();

            bof.Questions.ApproveQuestion(questionId, username);

            bof.SaveChanges();
        }
         
        public void RejectQuestion(int questionId, string rejectReason, string username)
        {
            var bof = GetBusinessObjectFactory();

            bof.Questions.RejectQuestion(questionId, rejectReason, username);

            bof.SaveChanges();
        }

        public void CorrectQuestion(int questionId, string title, string description)
        {
            var bof = GetBusinessObjectFactory();

            bof.Questions.CorrectQuestion(questionId, title, description);

            bof.SaveChanges();
        }

        public IEnumerable<Question> GetAll()
        {
            var bof = GetBusinessObjectFactory();
            
            return bof.Questions.GetAll();
        }

        public IEnumerable<Question> GetApprovedQuestions(int take, int skip)
        {
            var bof = GetBusinessObjectFactory();
            var questions = bof.Questions.GetApprovedQuestions(take, skip);
            return questions;
        }

        public IEnumerable<Question> GetNotApprovedQuestions(int take, int skip)
        {
            var bof = GetBusinessObjectFactory();
            var questions = bof.Questions.GetNotApprovedQuestions(take, skip);
            return questions;
        }

        public Question GetQuestionById(int id)
        {
            var bof = GetBusinessObjectFactory();

            return bof.Questions.GetQuestionById(id);
        }

        public bool CreateNewQuestion(Question question, ApplicationUser creator)
        {
            var bof = GetBusinessObjectFactory();

            bof.Questions.CreateQuestion(question, creator);

            return bof.SaveChanges() > 0;

        }

        public bool CreateNewQuestion(Question question, string creatorName)
        {
            var bof = GetBusinessObjectFactory();

            bof.Questions.CreateQuestion(question, creatorName);

            return bof.SaveChanges() > 0;

        }

        public IEnumerable<Question> GetQuestionsOfUser(string username, int take, int skip)
        {
            var bof = GetBusinessObjectFactory();
            var questions = bof.Questions.GetQuestionsOfUser(username, take, skip);
            return questions;
        }

        public string GetQuestionTitle(int questionId)
        {
            var bof = GetBusinessObjectFactory();

            return bof.Questions.GetQuestionTitle(questionId);
        }

        public IEnumerable<QuestionHeaderDTO> GetAnsweredQuestions(int skip, int take)
        {
            var bof = GetBusinessObjectFactory();

            return bof.Questions.GetAnsweredQuestions(skip, take);
        }

        public IEnumerable<QuestionCategory> GetQuestionCategories()
        {
            return GetBusinessObjectFactory().Questions.GetQuestionCategories();
        }

        public IEnumerable<QuestionHeaderDTO> FindQuestions(string text)
        {
            return GetBusinessObjectFactory().Questions.FindQuestions(text);
        }
    }
}
