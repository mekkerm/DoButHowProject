using System;
using Dbh.Model.EF.Context;
using Dbh.Model.EF.Interfaces;
using Dbh.Model.EF.Interfaces.Interfaces;
using Dbh.Model.EF.Repositories;

namespace Dbh.Model.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }

        private BlogRepository _blogRepo;
        private AppUserRepository _appUserRepo;
        public IBlogRepository Blogs {
            get {
                if (_blogRepo == null)
                {
                    _blogRepo = new BlogRepository(_context);
                }
                return _blogRepo;
            }
        }
        
        public IAppUserRepository AppUsers {
            get {
                if (_appUserRepo == null)
                {
                    _appUserRepo = new AppUserRepository(_context);
                }
                return _appUserRepo;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
