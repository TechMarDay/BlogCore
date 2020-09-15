using Common;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Users.Commands.UserCommands;
using Users.UI.Interfaces.Queries;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class UserController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IUserQueries userQueries = null;
        public UserController(IMediator mediator, IUserQueries userQueries)
        {
            this.mediator = mediator;
            this.userQueries = userQueries;
        }
        [HttpGet]
        [Route("getspage")]
        [AuthorizeInUserService(FeatureConstant.EmployeeManagement, Permissions.View)]
        public async Task<ApiResult> GetsPage(int CurrentPage, int PageSize, string SearchText, int InputViewMode)
        {
            var rs = await userQueries.GetsPage(CurrentPage, PageSize, SearchText, InputViewMode);

            return new ApiResult()
            {
                Result = rs == null ? -1 : 0,
                Data = rs
            };
        }

        #region UserAction

        [HttpGet]
        [Route("get")]
        [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.View)]
        public async Task<ApiResult> Get(int userId)
        {
            var rs = await userQueries.Get(userId);
            return new ApiResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.View)]
        public async Task<ApiResult> Gets()
        {
            var rs = await userQueries.Gets();
            return new ApiResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("gets/with-area")]
       [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.View)]
        public async Task<ApiResult> GetWithAreas(bool isExternal)
        {
            string cmd = string.Empty;
            cmd += $"RoleId IN (SELECT r.Id FROM Role r WHERE r.IsExternal = {(isExternal ? "1" : "0")})";
            var rs = await userQueries.Gets(cmd);
            return new ApiResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("get/userwithrole")]
        [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.View)]
        public async Task<ApiResult> GetUserWithRoles(int userId)
        {
            var rs = await userQueries.GetUserWithRole(userId);
            return new ApiResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("gets/userwithrole")]
        [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.View)]
        public async Task<ApiResult> GetUsersWithRoles(int userType = 0)
        {
            //0 : all
            //1 : externalUser
            //2 : InternalUser
            string cmd = string.Empty;
            if (userType == 1)
            {
                cmd = "IsExternalUser = 1";
            }
            else if (userType == 2)
            {
                cmd = "IsExternalUser = 0";
            }

            var rs = await userQueries.GetUsersWithRole(cmd);
            return new ApiResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("reset-password")]
       [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.Update)]
        public async Task<ApiResult> ResetPassword([FromBody]ResetPasswordByAdminCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("add")]
        [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.Insert, true)]
        public async Task<ApiResult> Add([FromBody]AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs > 0 ? 0 : -1,
                Data = rs
            };
        }

        [HttpPost]
        [Route("update")]
        [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.Update)]
        public async Task<ApiResult> Update([FromBody]UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.Delete)]
        public async Task<ApiResult> Delete([FromBody]DeleteCommand command)
        {
            return new ApiResult()
            {
                Result = 0,
                Data = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("update/password")]
       [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.Update)]
        public async Task<ApiResult> UpdatePassword([FromBody]UpdatePasswordCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update/roles")]
        [AuthorizeInUserService(FeatureConstant.UserManagement, Permissions.Update)]
        public async Task<ApiResult> ChangeRoles([FromBody]ChangeRolesCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs
            };
        }
        #endregion
    }
}
