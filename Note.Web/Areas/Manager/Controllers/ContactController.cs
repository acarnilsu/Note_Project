using Microsoft.AspNetCore.Mvc;
using Note.BusinessLayer.Abstract;
using Note.EntityLayer.Concrete;

namespace Note.Web.Areas.Manager.Controllers
{

    [Area("Manager")]
    public class ContactController : Controller
    {
        private readonly IMessageService _messageService;

        public ContactController(IMessageService messageService)
        {
            _messageService = messageService;
        }



        public IActionResult Index()
        {

            var result = _messageService.TGetAll().OrderByDescending(x => x.Status).ToList();


            return View(result);
        }


        [HttpGet]
        public IActionResult EditMessage(string id)
        {
            var result = _messageService.TGetByIdAsync(id).Result;
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> EditMessage(Message msg)
        {
            msg.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                 _messageService.TUpdate(msg);
            }

            return RedirectToAction("Index");
        }



    }
}
