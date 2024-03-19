using Microsoft.AspNetCore.Mvc;

namespace Modascon.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            return View();
        }

    }
}
