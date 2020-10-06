using BlogCore.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogCore.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class LoginController : Controller
    {
        private readonly BlogCoreContext _context;

        public LoginController(BlogCoreContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return View();
            }

            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            var user = await _context.User.Where(x => x.UserName == userName
            && x.PassWord == password)
                .FirstOrDefaultAsync();

            if(user != null)
            {
                //Admin
                if(user.Role == 1)
                {

                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("userName", user.UserName),
                    new Claim("userId", user.Id.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticated = true;
                
                }
            }

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Redirect("/Admins/dashboard");
            }

            return View();

        }
    }
}