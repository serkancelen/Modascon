using Entities.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create([FromForm] Contact contact)
        {
            _manager.ContactService.CreateContact(contact);
            return RedirectToAction("Teşekkürler.");
        }
    }
}
