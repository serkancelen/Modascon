using Entities.Dtos;
using Entities.Models;
using Iyzipay.Model.V2.Subscription;
using Microsoft.AspNetCore.Mvc;
using Modascon.Models;
using Repositories;
using Services.Contracts;

namespace Modascon.Controllers
{
    public class ContactController : Controller
    {
        private readonly IServiceManager _manager;

        public ContactController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] CreateContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                await _manager.ContactService.CreateContactAsync(contactDto);
                return RedirectToAction("Index");
            }
            return View(contactDto);
        }
    }
}
