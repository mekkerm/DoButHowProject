using Microsoft.EntityFrameworkCore;
using Dbh.Model.EF.Entities;


namespace Dbh.Model.EF.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        //public DbSet<ApplicationUser> AppUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-LVA752F;Initial Catalog=TestDb2;Integrated Security=True");
        }
        
    }
}
