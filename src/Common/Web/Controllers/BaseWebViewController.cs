using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers
{
    public abstract class BaseWebViewController : BaseController
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.LoginSession = LoginSession;
        }
    }
}
