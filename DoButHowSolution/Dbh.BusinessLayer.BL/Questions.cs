using Dbh.BusinessLayer.Contracts;
using Dbh.Elasticsearch.BL.DataIndexing;
using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dbh.BusinessLayer.BL
{
    public class Questions : BusinessObjectBase, IQuestions
    {
        private IIndexer<Question> _qestionIndexer;

        public Questions(IUnitOfWork uow)
            : base(uow)
        { }

        private IIndexer<Question> ESQuestionIndexer {
            get {
                if (_qestionIndexer == null)
                {
                    _qestionIndexer = new QuestionIndexer();
                }
                return _qestionIndexer;
            }
        }

        public void ApproveQuestion(int questionId, string username)
        {
            var oldQuestion = _uow.Questions.Get(questionId);
            var user = _uow.AppUsers.GetUserByName(username);

            oldQuestion.ApproveDate = DateTime.Now;
            oldQuestion.IsApproved = true;
            oldQuestion.IsRejected = false;
            oldQuestion.RejectReason = null;
            oldQuestion.ApproverId = user.Id;

            ESQuestionIndexer.IndexData(oldQuestion);
        }

        public void RejectQuestion(int questionId, string rejectReason, string username)
        {
            var oldQuestion = _uow.Questions.Get(questionId);
            var user = _uow.AppUsers.GetUserByName(username);

            oldQuestion.ApproveDate = null;
            oldQuestion.RejectDate = DateTime.Now;
            oldQuestion.IsApproved = false;
            oldQuestion.IsRejected = true;
            oldQuestion.RejectReason = rejectReason;
            oldQuestion.RejectorId = user.Id;
        }

        public void CorrectQuestion(int questionId, string title, string description)
        {
            var oldQuestion = _uow.Questions.Get(questionId);
            oldQuestion.Title = title;
            oldQuestion.Description = description;

            oldQuestion.ApproveDate = null;
            oldQuestion.RejectDate = null;
            oldQuestion.IsApproved = false;
            oldQuestion.IsRejected = false;
            oldQuestion.RejectReason = null;
        }

        public void CreateQuestion(Question question, ApplicationUser creator)
        {
            question.CreationDate = DateTime.Now;
            question.CreatorId = creator.Id;
            question.IsApproved = false;
            
            _uow.Questions.Add(question);
        }

        public void CreateQuestion(Question question, string creator)
        {
            var user = _uow.AppUsers.GetUserByName(creator);
            question.CreationDate = DateTime.Now;
            question.CreatorId = user.Id;
            question.IsApproved = false;

            _uow.Questions.Add(question);
        }

        public IEnumerable<Question> GetAll()
        {
            var questions = _uow.Questions.GetAll().ToList();
            foreach (var question in questions)
            {
                question.Creator = _uow.AppUsers.GetUser(question.CreatorId);
                
            }

            return questions;
        }

        public Question GetQuestionById(int id)
        {
            var question = _uow.Questions.Get(id);
            question.CategoryDescription = _uow.QuestionCategories.Get(question.CategoryId).Name;
            question.Creator = _uow.AppUsers.GetUser(question.CreatorId);
            return question;
        }

        public IEnumerable<Question> GetQuestionsOfUser(string username, int take, int skip)
        {
            var user = _uow.AppUsers.GetUserByName(username);
            var questions = _uow.Questions.FindAll(q => q.CreatorId == user.Id).Skip(skip).Take(take).ToList();
            foreach (var question in questions)
            {
                question.Creator = user;
            }
            return questions;
        }


        public IEnumerable<Question> GetNotApprovedQuestions(int take, int skip)
        {
            var questions = _uow.Questions.FindAll(q => !q.IsApproved && !q.IsRejected).Skip(skip).Take(take).ToList();
            foreach (var question in questions)
            {
                question.Creator = _uow.AppUsers.GetUser(question.CreatorId);
            }
            return questions;
        }

        public IEnumerable<Question> GetApprovedQuestions(int take, int skip)
        {
            var questions = _uow.Questions.FindAll(q => q.IsApproved).Skip(skip).Take(take).ToList();
            foreach (var question in questions)
            {
                question.Creator = _uow.AppUsers.GetUser(question.CreatorId);
            }
            return questions;
        }

        public string GetQuestionTitle(int questionId)
        {
            var question = _uow.Questions.Get(questionId);

            return question.Title;
        }

        public IEnumerable<Question> GetAnsweredQuestions(int skip, int take)
        {
            var categories = _uow.QuestionCategories.GetAll();

            var questions =_uow.Questions.FindAll(q => bool.Equals(q.HasAnwser, true)).Skip(skip).Take(take);
            foreach (var question in questions)
            {
                question.CategoryDescription = categories.FirstOrDefault(x => x.Id == question.CategoryId).Name;
            }

            return questions;
        }

        public IEnumerable<QuestionCategory> GetQuestionCategories()
        {
            return _uow.QuestionCategories.GetAll();
        }
    }
}
