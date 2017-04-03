﻿using Dbh.Model.EF.Interfaces.Interfaces;
using System;

namespace Dbh.Model.EF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogRepository Blogs { get; }

        IAppUserRepository AppUsers { get; }
        int SaveChanges();
    }
}
