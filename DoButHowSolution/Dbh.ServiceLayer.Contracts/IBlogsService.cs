using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.ServiceLayer.Contracts
{
    public interface IBlogsService
    {
        Blog Create(Blog blog);

        void JustDoSomething(Blog blog); 
    }
}
