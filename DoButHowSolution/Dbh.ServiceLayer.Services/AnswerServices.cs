using Dbh.BusinessLayer.Contracts;
using Dbh.Common.IoCContainer;
using Dbh.Model.EF.Entities;
using Dbh.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dbh.ServiceLayer.Services
{
    public class AnswerServices : IAnswerServices
    {

        public bool AnswerQuestion(int questionId, string answer, string currentUser)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();
            try
            {
                var result = businessUoW.Answers.CreateAnswer(questionId, answer, currentUser);

            }catch(BusinessException ex)
            {
                throw new ServiceException(ex);
            }

            return businessUoW.SaveChanges() > 0;
        }

        public List<Answer> GetNotApprovedAnswers()
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            return businessUoW.Answers.GetNotApprovedAnswers().ToList();
        }

        public Answer GetAnswerById(int id)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            return businessUoW.Answers.GetAnswerById(id);
        }

        public void ApproveAnswer(int answerId, string username)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            businessUoW.Answers.ApproveAnswer(answerId, username);

            businessUoW.SaveChanges();
        }

        public void RejectAnswer(int answerId, string rejectReason, string username)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            businessUoW.Answers.RejectAnswer(answerId, rejectReason, username);

            businessUoW.SaveChanges();
        }

        public List<Answer> GetAnswers(int skip, int take)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            return businessUoW.Answers.GetAnswers(skip, take).ToList();
        }

        public List<Answer> GetAnswersOfQuestion(int questionId)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            return businessUoW.Answers.GetAnswersOfQuestion(questionId).ToList();
        }

        public List<Answer> GetAnswersOfUser(string username)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            return businessUoW.Answers.GetAnswersOfUser(username).ToList();
        }

        public List<Answer> GetRejectedQuestionsOfUser(string username)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            return businessUoW.Answers.GetRejectedAnswersOfUser(username).ToList();
        }
    }
}
