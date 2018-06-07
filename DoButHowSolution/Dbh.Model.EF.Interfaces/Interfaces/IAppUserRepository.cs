using Dbh.Model.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.Model.EF.Interfaces
{
    public interface IAppUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetUser(string id);

        ApplicationUser GetUserByName(string name);

        string GetUserIdByName(string name);

        


    }
}
