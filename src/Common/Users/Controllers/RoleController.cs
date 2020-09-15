using Common;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Users.Commands.RoleCommands;
using Users.UI.Interfaces.Queries;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class RoleController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IRoleQueries roleQueries = null;
        public RoleController(IMediator mediator, IRoleQueries roleQueries)
        {
            this.mediator = mediator;
            this.roleQueries = roleQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeInUserService(FeatureConstant.RoleManagement, Permissions.View)]
        public async Task<ApiResult> Gets()
        {
            return new ApiResult()
            {
                Result = 0,
                Data = await roleQueries.Gets()
            };
        }

        [HttpGet]
        [Route("list")]
        [AuthorizeInUserService(FeatureConstant.RoleManagement, Permissions.View)]
        public async Task<ApiResult> List(int InputCurrentPage, int InputPageSize, string SearchText, int InputViewMode)
        {
            return new ApiResult()
            {
                Result = 0,
                Data = await roleQueries.List(InputCurrentPage, InputPageSize, SearchText, InputViewMode)
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeInUserService(FeatureConstant.RoleManagement, Permissions.View)]
        public async Task<ApiResult> Gets(int roleId)
        {
            return new ApiResult()
            {
                Result = 0,
                Data = await roleQueries.Get(roleId)
            };
        }

        [HttpPost]
        [Route("add")]
        [AuthorizeInUserService(FeatureConstant.RoleManagement, Permissions.Insert)]
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
        [AuthorizeInUserService(FeatureConstant.RoleManagement, Permissions.Update)]
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
        [AuthorizeInUserService(FeatureConstant.RoleManagement, Permissions.Delete)]
        public async Task<ApiResult> Delete([FromBody]DeleteCommand command)
        {
            return new ApiResult()
            {
                Result = 0,
                Data = await mediator.Send(command)
            };
        }
    }
}
