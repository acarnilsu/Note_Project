using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Note.BusinessLayer.Abstract;
using Note.EntityLayer.Concrete;
using Note.Web.ViewModels;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;

namespace Note.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Admin,Editör,Stajyer")]

    public class NoteController : Controller
    {
        private readonly IAppNoteService _appNoteService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;


        public NoteController(IAppNoteService appNoteService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _appNoteService = appNoteService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = _appNoteService.TNotesWithAuthor().OrderByDescending(x => x.CreatedDate).ToList();
            return View(_mapper.Map<List<AppNoteListVM>>(values));
        }

        public async Task<IActionResult> MyNote()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var result = _appNoteService.TNotesWithAuthorId(user.Id).OrderByDescending(x => x.CreatedDate).ToList();

            return View(_mapper.Map<List<AppNoteListVM>>(result));
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppNote appNote, IFormFile userPicture)
        {
            appNote.ID = Guid.NewGuid().ToString();   //guid=uniqe bir değer atıyor.
            appNote.CreatedDate = DateTime.Now;
            appNote.ModifiedDate = DateTime.Now;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            appNote.AppUserId = user.Id;
            appNote.AppUser = user;
            if (ModelState.IsValid)
            {

                if (userPicture != null && userPicture.Length > 0)
                {

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/noteImages", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);
                        appNote.ImageUrl = "/noteImages/" + fileName;
                    }


                }

                await _appNoteService.TAddAsync(appNote);
                return RedirectToAction("Index");
            }

            return View(appNote);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var values = await _appNoteService.TGetByIdAsync(id);
            _appNoteService.TRemove(values);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var values = await _appNoteService.TGetByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AppNote appNote, IFormFile userPicture)
        {

            appNote.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {

                if (userPicture != null && userPicture.Length > 0)
                {
                    if (appNote.ImageUrl != null)
                    {
                        string oldImagePath = $"{Directory.GetCurrentDirectory()}\\wwwroot{appNote.ImageUrl}";

                        System.IO.File.Delete(oldImagePath);
                    }


                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/NoteImages", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);
                        appNote.ImageUrl = "/noteImages/" + fileName;
                    }


                }

                _appNoteService.TUpdate(appNote);
                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", "Bir hata oluştu");
            return View(appNote);

        }


        public IActionResult StaticPdfReport(string content)
        {
            var fileName= Guid.NewGuid().ToString();
            var result = _appNoteService.TGetByIdAsync(content).Result;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdffiles/" + fileName + ".pdf");
            var stream = new FileStream(path, FileMode.Create);
          
            Regex htmlReg = new Regex("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");

            string htmlString = htmlReg.Replace(result.Content, "");
            Document document = new Document(PageSize.A4,25,25,50,3);
            PdfWriter.GetInstance(document, stream);
            document.Open();
            Paragraph paragraph = new Paragraph(htmlString);

            document.Add(paragraph);
            document.Close();
            return File("/pdffiles/" + fileName + ".pdf", "application /pdf", fileName + ".pdf");
        }
    }
}
