using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoWebapp.Controllers
{
    // BaseController để chặn các trang nếu chưa login
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Lấy session user
            var user = context.HttpContext.Session.GetString("user");

            // Nếu chưa login → redirect về Account/Login
            if (string.IsNullOrEmpty(user))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }

            base.OnActionExecuting(context);
        }
    }
}