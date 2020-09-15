using Common;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Users.Commands.RolePermissionCommands;
using Users.UI.Interfaces.Queries;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class RolePermissionController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IRolePermissionQueries rolePermissionQueries = null;
        public RolePermissionController(IMediator mediator, IRolePermissionQueries rolePermissionQueries)
        {
            this.mediator = mediator;
            this.rolePermissionQueries = rolePermissionQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeInUserService(FeatureConstant.RoleManagement, Common.Permissions.View)]
        public async Task<ApiResult> Gets()
        {
            return new ApiResult()
            {
                Result = 0,
                Data = await rolePermissionQueries.Gets()
            };
        }

        [HttpGet]
        [Route("gets/by-role")]
        [AuthorizeInUserService(FeatureConstant.RoleManagement, Common.Permissions.View)]
        public async Task<ApiResult> Gets(int roleId)
        {
            return new ApiResult()
            {
                Result = 0,
                Data = await rolePermissionQueries.Gets(roleId)
            };
        }

        [HttpPost]
        [Route("update")]
        [AuthorizeInUserService(FeatureConstant.RoleManagement, Common.Permissions.Update)]
        public async Task<ApiResult> UpdateUser([FromBody]UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new ApiResult()
            {
                Result = rs
            };
        }
    }
}
