using Dbh.Model.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Dbh.Model.EF.Interfaces;
using System.Collections.Generic;

namespace Dbh.Model.EF.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Question> GetNotApprovedQuestions()
        {
            return base.Find(q => ! q.IsApproved);
        }

        public IEnumerable<Question> GetApprovedQuestions()
        {
            return base.Find(q => q.IsApproved);
        }

        public IEnumerable<Question> GetQuestionOfUser(ApplicationUser user)
        {
            return base.Find(q => q.CreatorId == user.Id);
        }

        public IEnumerable<Question> GetQuestionOfUser(string UserId )
        {
            return base.Find(q => q.CreatorId == UserId);
        }


    }
}
