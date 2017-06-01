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

            businessUoW.Answers.CreateAnswer(questionId, answer, currentUser);

            return businessUoW.SaveChanges() > 0;
        }

        public List<Answer> GetNotApprovedAnswers()
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            return businessUoW.Answers.GetNotApprovedAnswers().ToList();
        }
    }
}
