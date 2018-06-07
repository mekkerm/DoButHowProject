using Dbh.Model.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Dbh.Model.EF.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dbh.Model.EF.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(DbContext context) : base(context) { _ctx = context; }
        private readonly DbContext _ctx;

        public IEnumerable<Question> GetNotApprovedQuestions()
        {
            return base.FindAll(q => ! q.IsApproved);
        }

        public IEnumerable<Question> GetApprovedQuestions()
        {
            return base.FindAll(q => q.IsApproved);
        }

        public IEnumerable<Question> GetQuestionOfUser(ApplicationUser user)
        {
            return base.FindAll(q => q.CreatorId == user.Id);
        }

        public IEnumerable<Question> GetQuestionOfUser(string UserId )
        {
            return base.FindAll(q => q.CreatorId == UserId);
        }

        public async Task<IEnumerable<Question>> GetAllWithCreators()
        {
            return await _dbSet.Include(q => q.Creator).ToListAsync();
        }
        
    }
}
