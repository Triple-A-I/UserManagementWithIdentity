namespace UserManagementWithIdentity.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }

        public List<RolesViewModel> Roles { get; set; }
    }
}
