using Common.Exceptions;
using Common.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Users.UI.Models;
using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        private readonly IRoleQueries roleQueries = null;
        private readonly IUserRoleRepository userRoleRepository = null;
        public AddCommandHandler(IUserRepository userRepository, IUserQueries userQueries, IRoleQueries roleQueries, IUserRoleRepository userRoleRepository)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
            this.roleQueries = roleQueries;
            this.userRoleRepository = userRoleRepository;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            // Role is null that this action is made by the staff. But LoginSession must not null
            if (request.LoginSession == null && request.RoleId == 0)
            {
                throw new NotPermissionException();
            }

            //check existed username
            var user = await userQueries.Get(request.UserName);
            if (user != null)
            {
                throw new BusinessException("ExistedAccount");
            }

            //check existed email
            user = await userQueries.GetWithEmail(request.Email);
            if (user != null)
            {
                throw new BusinessException("ExistedEmail");
            }

            //Add user
            Guid securityPassword = Guid.NewGuid();
            var userId = await this.userRepository.Add(new UserAccount()
            {
                Username = request.UserName,
                Password = (request.Password.Trim() + securityPassword.ToString()).CalculateMD5Hash(),
                Email = request.Email,
                DisplayName = request.DisplayName,
                PhoneNumber = request.PhoneNumber,
                SecurityPassword = securityPassword,
                IsExternalUser = request.RoleId != 0,
                IsActived = request.RoleId == 0,
                IsUsed = request.RoleId != 0 || request.IsUsed,
                CreatedDate = DateTime.Now
            }, request.LoginSession == null ? null : (int?)request.LoginSession.UserId);

            // for who is't the staff
            if (userId > 0 && request.RoleId != 0)
            {
                var role = await roleQueries.Get(request.RoleId);
                if (role != null && role.IsExternalRole)
                {
                    await userRoleRepository.Add(new UserAccountRole()
                    {
                        UserId = userId,
                        RoleId = role.Id
                    });
                }
                else
                {
                    throw new BusinessException("Role.NotExisted");
                }
            }

            //Send email for active here


            return userId;
        }
    }
}
