using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Modascon.Components
{
    public class ContactSummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public ContactSummaryViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public IViewComponentResult Invoke()
        {
            var contacts = _manager.ContactService.GetAllContacts(false);
            return View(contacts);
        }
    }
}
