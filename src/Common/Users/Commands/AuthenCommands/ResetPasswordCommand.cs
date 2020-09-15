using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class ResetPasswordCommand : BaseCommand<int>
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string PinCode { set; get; }

        public ResetPasswordCommand(string userName, string password, string pinCode)
        {
            UserName = userName;
            Password = password;
            PinCode = pinCode;
        }
    }
}