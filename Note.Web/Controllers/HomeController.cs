using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Note.BusinessLayer.Abstract;
using Note.EntityLayer.Concrete;
using Note.Web.Models;
using Note.Web.ViewModels;
using System.Diagnostics;
using System.Linq;

namespace Note.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppNoteService _appNoteService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;


        public HomeController(IAppNoteService appNoteService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _appNoteService = appNoteService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = _appNoteService.TNotesWithAuthor().Where(x=>x.Status==true).OrderByDescending(x => x.CreatedDate).ToList();

            return View(_mapper.Map<List<AppNoteListVM>>(values));
        }





        public async Task<IActionResult> Detail(string id)
        {
            DetailNoteVM model = new();

            AppNote values = await _appNoteService.TGetByIdAsync(id);

            var user = await _userManager.FindByIdAsync(values.AppUserId);

            model.Surname = user.Surname;
            model.Name = user.Name;

            return View(_mapper.Map(values, model));
        }



        [Authorize(Policy = "AnkaraPolicy")]
        public IActionResult AnkaraPage()
        {
            var values = _appNoteService.TNotesWithAuthor().Where(x => x.Status == false).OrderByDescending(x => x.CreatedDate).ToList();

            return View(_mapper.Map<List<AppNoteListVM>>(values));
        }



    }
}