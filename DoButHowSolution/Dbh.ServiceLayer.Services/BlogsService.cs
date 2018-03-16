using Dbh.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Dbh.Model.EF.Entities;
using Dbh.BusinessLayer.Contracts;
using Dbh.Common.IoCContainer;

namespace Dbh.ServiceLayer.Services
{
    public class BlogsService : ServiceBase, IBlogsService
    {
        public Blog Create(Blog blog)
        {
            var businessUoW = GetUoW();

            return businessUoW.Blogs.DoWhateverYouWant();
        }

        public void JustDoSomething(Blog blog)
        {
            var businessUoW = GetUoW();

            businessUoW.Blogs.DoWhateverYouWant();
        }
    }
}
