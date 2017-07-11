using Dbh.BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Dbh.Model.EF.Interfaces;
using Dbh.Model.EF.Entities;
using System.Linq;

namespace Dbh.BusinessLayer.BL
{
    public class Answers : BusinessObjectBase, IAnswers
    {
        public Answers(IUnitOfWork uow)
            : base(uow)
        { }

        public bool CreateAnswer(int questionId, string response, string creatorName)
        {
            var user = _uow.AppUsers.GetUserByName(creatorName);

            var answers = _uow.Answers.Find(a => a.CreatorId == user.Id && a.QuestionId == questionId).ToList();
            //if (answers.Any())
            //{
            //    throw new BusinessException("You have anwsered already this question!");
            //}

            var answer = new Answer();
            answer.QuestionId = questionId;
            answer.Response = response;
            answer.CreationDate = DateTime.Now;
            answer.CreatorId = user.Id;
            answer.IsApproved = false;
            answer.IsRejected = false;

            _uow.Answers.Add(answer);
            return true;
            
        }

        public IEnumerable<Answer> GetNotApprovedAnswers()
        {
            var answers = _uow.Answers.Find(q => !q.IsApproved && !q.IsRejected);
            foreach (var answer in answers)
            {
                answer.Creator = _uow.AppUsers.GetUser(answer.CreatorId);
            }
            return answers;
        }

        public Answer GetAnswerById(int id)
        {
            var answer = _uow.Answers.Get(id);

            answer.Creator = _uow.AppUsers.GetUser(answer.CreatorId);
            
            return answer;
        }

        public void ApproveAnswer(int answerId, string username)
        {
            var answer = _uow.Answers.Get(answerId);
            var question = _uow.Questions.Get(answer.QuestionId);
            
            if (!answer.IsApproved && !answer.IsRejected)
            {
                var approver = _uow.AppUsers.GetUserByName(username);

                answer.ApproveDate = DateTime.Now;
                answer.ApproverId = approver.Id;
                answer.IsApproved = true;
                answer.IsRejected = false;
                answer.RejectReason = null;

                question.HasAnwser = true;
            }
            
        }

        public void RejectAnswer(int answerId, string rejectReason, string username)
        {
            var answer = _uow.Answers.Get(answerId);
            if (!answer.IsRejected)
            {
                var rejector = _uow.AppUsers.GetUserByName(username);

                answer.RejectDate = DateTime.Now;
                answer.RejectorId = rejector.Id;
                answer.IsApproved = false;
                answer.IsRejected = true;
                answer.RejectReason = rejectReason;
                answer.ApproveDate = null;
            }
        }

        public IEnumerable<Answer> GetAnswers(int skip, int take)
        {
            return _uow.Answers.Find(a => a.IsApproved).Skip(skip).Take(take);
        }

        public IEnumerable<Answer> GetAnswersOfQuestion(int questionId)
        {
            return _uow.Answers.Find(a => a.QuestionId == questionId && a.IsApproved);
        }

        public IEnumerable<Answer> GetAnswersOfUser(string username)
        {
            var user = _uow.AppUsers.GetUserByName(username);

            return _uow.Answers.Find(a => a.CreatorId == user.Id);
        }

        public IEnumerable<Answer> GetRejectedAnswersOfUser(string username)
        {
            var user = _uow.AppUsers.GetUserByName(username);

            return _uow.Answers.Find(a => a.CreatorId == user.Id && a.IsRejected);
        }
    }
}
