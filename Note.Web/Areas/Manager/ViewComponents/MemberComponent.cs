using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Note.EntityLayer.Concrete;
using Note.Web.Areas.Manager.ViewModels;

namespace Note.Web.Areas.Manager.ViewComponents
{
    public class MemberComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
     
        private readonly IMapper _mapper;

        public MemberComponent(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            MemberComponentAppUserVM viewModel = _mapper.Map<MemberComponentAppUserVM>(user);


            return View(viewModel);
        }
    }
    
}
