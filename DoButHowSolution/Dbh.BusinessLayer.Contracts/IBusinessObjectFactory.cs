using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.Contracts
{
    public interface IBusinessObjectFactory
    {
        IBlogs Blogs { get; }


        IQuestions Questions { get; }

        int SaveChanges();
    }
}
