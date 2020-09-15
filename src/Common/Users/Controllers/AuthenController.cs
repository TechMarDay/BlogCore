using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Users.Commands.AuthenCommands;
using Users.UI.Interfaces.Queries;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthenController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IAuthenQueries authenQueries = null;
        private readonly IUserQueries userQueries = null;
        public AuthenController(IMediator mediator, IAuthenQueries authenQueries, IUserQueries userQueries)
        {
            this.mediator = mediator;
            this.authenQueries = authenQueries;
            this.userQueries = userQueries;
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeInUserService]
        public async Task<ApiResult> Get()
        {
            var user = await userQueries.Get(LoginSession.UserId);
            return new ApiResult()
            {
                Result = user == null ? -1 : 0,
                Data = user
            };
        }

        [HttpPost]
        [Route("login")]
        public async Task<ApiResult> Login([FromBody]LoginCommand command)
        {
            var rs = await mediator.Send(command);
            if (rs.LoginResult == 0)
            {
                SetUserSession(rs);
            }
            return new ApiResult()
            {
                Result = rs.LoginResult,
                Data = rs.LoginResult == 0 ? rs : null,
                ErrorMessage = rs.LoginResult == 0 ? null : GetCaption(rs.LoginCaptionMessage)
            };
        }

        [HttpPost]
        [Route("loginanotheruser")]
        [AuthorizeInUserService]
        public async Task<ApiResult> LoginAnotherUser([FromBody]LoginAnotherCommand command)
        {
            var rs = await mediator.Send(command);
            if (rs.LoginResult == 0)
            {
                SetUserSession(rs);
                RemoveUserSession(LoginSession);
            }
            return new ApiResult()
            {
                Result = rs.LoginResult,
                Data = rs.LoginResult == 0 ? rs : null,
                ErrorMessage = rs.LoginResult == 0 ? null : GetCaption(rs.LoginCaptionMessage)
            };
        }

        [HttpPost]
        [Route("logout")]
        [AuthorizeInUserService(isDontNeedLogin: true)]
        public async Task<ApiResult> Logout([FromBody]LogoutCommand command)
        {
            var rs = await mediator.Send(command);
            if (rs == 0)
            {
                RemoveUserSession(LoginSession);
            }
            return new ApiResult()
            {
                Result = rs
            };
        }

        [HttpGet]
        [Route("checklogin")]
        public async Task<ApiResult> CheckLogin(string accessToken)
        {
            var userSession = GetUserSession(accessToken);
            if (userSession == null)
            {
                userSession = await authenQueries.Get(accessToken);
                if (userSession != null)
                {
                    SetUserSession(userSession);
                }
            }
            return new ApiResult()
            {
                Result = userSession != null ? 0 : -1,
                Data = userSession
            };
        }


        [HttpPost]
        [Route("changepassword")]
        [AuthorizeInUserService]
        public async Task<ApiResult> ChangePassword([FromBody]ChangePasswordCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("registerresetpassword")]
        public async Task<ApiResult> RegisterResetPassword([FromBody]RegisterResetPasswordCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<ApiResult> ResetPassword([FromBody]ResetPasswordCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("active")]
        public async Task<ApiResult> ActiveUser([FromBody]ActiveUserPasswordCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs
            };
        }
    }
}
