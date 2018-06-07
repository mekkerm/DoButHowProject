using Dbh.Model.EF.Interfaces;
using System;

namespace Dbh.Model.EF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IQuestionRepository Questions { get; }

        IQuestionCategoryRepository QuestionCategories { get; }

        IAppUserRepository AppUsers { get; }

        IAnswerRepository Answers { get; }

        IAnswerRatingsRepository AnswerRatings { get; }
        int SaveChanges();
    }
}
