using Dbh.Model.EF.Interfaces.Interfaces;
using System;

namespace Dbh.Model.EF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogRepository Blogs { get; }

        IQuestionRepository Questions { get; }

        IAppUserRepository AppUsers { get; }

        IAnswerRepository Answers { get; }
        int SaveChanges();
    }
}
