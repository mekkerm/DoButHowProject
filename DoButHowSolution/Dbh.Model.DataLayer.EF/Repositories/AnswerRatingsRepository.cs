using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Interfaces;
using Dbh.Model.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.Model.EF.Repositories
{
    public class AnswerRatingsRepository : GenericRepository<AnswerRatings>, IAnswerRatingsRepository
    {
        private DbContext _ctx;
        public AnswerRatingsRepository(DbContext context) : base(context)
        {
            _ctx = context;
        }
    }
}
