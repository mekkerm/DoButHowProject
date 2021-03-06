﻿using Dbh.Model.EF.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dbh.Model.EF.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        IEnumerable<Question> GetNotApprovedQuestions();

        IEnumerable<Question> GetApprovedQuestions();

        Task<IEnumerable<Question>> GetAllWithCreators();

    }
}
