using Dbh.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Dbh.Model.EF.Entities;
using Dbh.BusinessLayer.Contracts;
using Dbh.Common.IoCContainer;

namespace Dbh.ServiceLayer.Services
{
    public class BlogsService : IBlogsService
    {
        public Blog Create(Blog blog)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            return businessUoW.Blogs.DoWhateverYouWant();
        }

        public void JustDoSomething(Blog blog)
        {
            var businessUoW = Resolver.Get<IBusinessObjectFactory>();

            businessUoW.Blogs.DoWhateverYouWant();
        }
    }
}
