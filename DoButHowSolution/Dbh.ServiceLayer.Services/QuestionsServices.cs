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
    public class QuestionServices: IQuestionServices
    {

        public void ApproveQuestion(Question question, ApplicationUser approver)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            businessUoW.Questions.ApproveQuestion(question, approver);

            businessUoW.SaveChanges();
        }

        public IEnumerable<Question> GetAll()
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();
            
            return businessUoW.Questions.GetAll();
        }

        public IEnumerable<Question> GetApprovedQuestions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetNotApprovedQuestions()
        {
            throw new NotImplementedException();
        }

        public Question GetQuestionById(int id)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            return businessUoW.Questions.GetQuestionById(id);
        }

        public bool CreateNewQuestion(Question question, ApplicationUser creator)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            businessUoW.Questions.CreateQuestion(question, creator);

            return businessUoW.SaveChanges() > 0;

        }

        public IEnumerable<Question> GetQuestionsOfUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
