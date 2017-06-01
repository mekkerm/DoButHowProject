using Dbh.BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Dbh.Model.EF.Interfaces;
using Dbh.Model.EF.Entities;

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
    }
}
