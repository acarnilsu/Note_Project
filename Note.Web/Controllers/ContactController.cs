using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Note.BusinessLayer.Abstract;
using Note.EntityLayer.Concrete;
using Note.Web.ViewModels;

namespace Note.Web.Controllers
{
    public class ContactController : Controller
    {

        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ContactController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MessageVM viewModel)
        {

            viewModel.Id = Guid.NewGuid().ToString();
            viewModel.Status = true;
            viewModel.CreatedDate = DateTime.Now;
            

            if (ModelState.IsValid)
            {
                Message mesaj = new();
                mesaj.ModifiedDate=DateTime.Now;
                await _messageService.TAddAsync(_mapper.Map(viewModel, mesaj));

                ViewBag.Message = "Mesajınız Gönderilmiştir";
                return Json(new { isSuccess = "True" });

            }
            else
            {


                return BadRequest(viewModel);

            }
        }
    }
}
