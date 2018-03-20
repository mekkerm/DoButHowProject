using Dbh.Model.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Dbh.Model.EF.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dbh.Model.EF.Repositories
{
    public class QuestionCategoryRepository : GenericRepository<QuestionCategory>, IQuestionCategoryRepository
    {
        private DbContext _ctx;
        public QuestionCategoryRepository(DbContext context) : base(context)
        {
            _ctx = context;
        }
    }
}
