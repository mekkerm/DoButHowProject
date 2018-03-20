using Dbh.BusinessLayer.Contracts;
using Dbh.Model.EF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.BL
{
    public class BusinessObjectFactory : IBusinessObjectFactory
    {
        private IUnitOfWork _uow;

        public BusinessObjectFactory(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private IQuestions _questions;
        public IQuestions Questions {
            get {
                if (_questions == null)
                {
                    _questions = new Questions(_uow);
                }

                return _questions;
            }
        }

        private IAnswers _answers;
        public IAnswers Answers {
            get {
                if (_answers == null)
                {
                    _answers = new Answers(_uow);
                }

                return _answers;
            }
        }

        public int SaveChanges()
        {
            return _uow.SaveChanges();
        }
    }
}
