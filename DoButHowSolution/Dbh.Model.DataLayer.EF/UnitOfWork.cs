using System;
using Dbh.Model.EF.Context;
using Dbh.Model.EF.Interfaces;
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
        
        private AppUserRepository _appUserRepo;
        private QuestionRepository _questionRepo;
        private AnswerRepository _answerRepo;
        private AnswerRatingsRepository _answerRatingsRepo;
        private QuestionCategoryRepository _questionCategories;
        
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

        public IAnswerRatingsRepository AnswerRatings {
            get {
                if (_answerRatingsRepo == null)
                {
                    _answerRatingsRepo = new AnswerRatingsRepository(_context);
                }
                return _answerRatingsRepo;
            }
        }


        public IQuestionCategoryRepository QuestionCategories {
            get {
                if (_questionCategories == null)
                {
                    _questionCategories = new QuestionCategoryRepository(_context);
                }
                return _questionCategories;
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
