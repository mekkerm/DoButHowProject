using Dbh.BusinessLayer.Contracts;
using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Dbh.BusinessLayer.BL
{
    public class Blogs : BusinessObjectBase, IBlogs

    {
        public Blogs(IUnitOfWork uow)
            : base(uow)
        { }

        public Blog doWhateverYouWant()
        {
            return _uow.Blogs.SingleOrDefault(null);
        }
    }
}
