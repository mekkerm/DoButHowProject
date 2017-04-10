using Dbh.BusinessLayer.Contracts;
using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Interfaces;
using System;
using System.Collections.Generic;

namespace Dbh.BusinessLayer.BL
{
    public class Questions : BusinessObjectBase, IQuestions
    {
        public Questions(IUnitOfWork uow)
            : base(uow)
        { }

        public void ApproveQuestion(Question question, ApplicationUser approver)
        {
            question.ApproveDate = DateTime.Now;
            question.Approver = approver;
            question.IsApproved = true;
        }

        public void CreateQuestion(Question question, ApplicationUser creator)
        {
            question.CreationDate = DateTime.Now;
            question.CreatorId = creator.Id;
            question.IsApproved = false;
            
            _uow.Questions.Add(question);
        }

        public IEnumerable<Question> GetAll()
        {
            return _uow.Questions.GetAll();
        }

        public Question GetQuestionById(int id)
        {
            return _uow.Questions.Get(id);
        }
    }
}
