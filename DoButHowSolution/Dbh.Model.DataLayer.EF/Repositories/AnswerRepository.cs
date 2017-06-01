using Dbh.Model.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Dbh.Model.EF.Interfaces;
using Dbh.Model.EF.Entities;

namespace Dbh.Model.EF.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        private DbContext _ctx;
        public AnswerRepository(DbContext context) : base(context)
        {
            _ctx = context;
        }
    }
}
