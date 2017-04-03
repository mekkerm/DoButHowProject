using Dbh.Model.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Dbh.Model.EF.Interfaces;

namespace Dbh.Model.EF.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(DbContext context) : base(context)
        {
        }
    }
}
