using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public interface IBusinessObjectFactory
    {
        IQuestions Questions { get; }
        IAnswers Answers { get; }
        int SaveChanges();
    }
}
