using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class AddCommand : BaseCommand<int>
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
        public string DisplayName { set; get; }
        public string PhoneNumber { set; get; }
        public bool IsUsed { set; get; }
        public int RoleId { set; get; } //for customer or comsumer (is't a staff)

        public AddCommand(string userName, string password, string email, int roleId)
        {
            UserName = userName;
            Password = password;
            Email = email;
            RoleId = roleId;
        }
    }
}