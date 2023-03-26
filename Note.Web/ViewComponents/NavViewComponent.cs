using Microsoft.AspNetCore.Mvc;

namespace Note.Web.ViewComponents
{
    public class NavViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
