namespace MVCWebClient.Models.AccountViewModels
{
    public class ChangeUserRoleViewModel
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string CurrentRole { get; set; }

        public bool IsUser { get; set; }

        public bool IsModerator { get; set; }

        public bool IsAdmin { get; set; }
    }
}
