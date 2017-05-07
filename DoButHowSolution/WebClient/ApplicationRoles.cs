using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Roles
{
    
    public enum UserRoles { Admin = 3, Moderator = 2, User = 1 };

    public class RoleUtils
    {
        RoleUtils()
        {
            
        }

        public static string GetRoleNames()
        {
            return String.Join(", ", Enum.GetNames(typeof(UserRoles)));
        }

        public static string[] GetRoleNameArray()
        {
            return Enum.GetNames(typeof(UserRoles));

            
        }



    }

}
