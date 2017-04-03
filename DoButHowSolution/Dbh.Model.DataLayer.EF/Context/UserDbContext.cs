﻿using Microsoft.EntityFrameworkCore;
using Dbh.Model.EF.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dbh.Model.EF.Context
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public UserDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-LVA752F;Initial Catalog=TestDb2;Integrated Security=True");
        }
        
    }
}
