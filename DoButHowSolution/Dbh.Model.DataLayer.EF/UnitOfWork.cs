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
        private readonly UserDbContext _userContext;

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
            _userContext = new UserDbContext();
        }

        private BlogRepository _blogRepo;
        private AppUserRepository _appUserRepo;
        private QuestionRepository _questionRepo;
        private AnswerRepository _answerRepo;

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
                    _appUserRepo = new AppUserRepository(_userContext);
                }
                return _appUserRepo;
            }
        }

        public IQuestionRepository Questions {
            get {
                if (_questionRepo == null)
                {
                    _questionRepo = new QuestionRepository(_context);
                }
                return _questionRepo;
            }
        }

        public IAnswerRepository Answers {
            get {
                if (_answerRepo == null)
                {
                    _answerRepo = new AnswerRepository(_context);
                }
                return _answerRepo;
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
