using System;
using Dbh.BusinessLayer.Contracts;
using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Interfaces;

namespace Dbh.BusinessLayer.BL
{
    public class Blogs : BusinessObjectBase, IBlogs

    {
        public Blogs(IUnitOfWork uow)
            : base(uow)
        { }
        

        public Blog DoWhateverYouWant()
        {
            return _uow.Blogs.SingleOrDefault(null);
        }
    }
}
