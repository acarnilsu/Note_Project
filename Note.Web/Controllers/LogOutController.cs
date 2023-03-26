using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Note.EntityLayer.Concrete;

namespace Note.Web.Controllers
{
    public class LogOutController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LogOutController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("LogIn","LogIn");
      
        }
    }
}
