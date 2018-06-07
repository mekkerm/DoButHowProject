using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Models.AccountViewModels
{
    public class ChangeUsersViewModel
    {
        public ChangeUsersViewModel()
        {
            Users = new List<ChangeUserRoleViewModel>();
            Moderators = new List<ChangeUserRoleViewModel>();
            Administrators = new List<ChangeUserRoleViewModel>();

            Roles = new List<String>();
        }
        public List<ChangeUserRoleViewModel> Users { get; set; }
        public List<ChangeUserRoleViewModel> Moderators { get; set; }
        public List<ChangeUserRoleViewModel> Administrators { get; set; }

        public List<String> Roles { get; set; }
    }
}
