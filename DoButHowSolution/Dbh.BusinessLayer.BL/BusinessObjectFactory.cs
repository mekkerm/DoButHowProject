using Dbh.BusinessLayer.Contracts;
using Dbh.Model.EF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.BusinessLayer.BL
{
    public class BusinessObjectFactory : IBusinessObjectFactory
    {
        private IUnitOfWork _uow;

        public BusinessObjectFactory(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private IBlogs _blogs;
        public IBlogs Blogs {
            get {
                if (_blogs == null)
                {
                    _blogs = new Blogs(_uow);
                }

                return _blogs;
            }
        }
        

        public void SaveChanges()
        {
            _uow.SaveChanges();
        }
    }
}
