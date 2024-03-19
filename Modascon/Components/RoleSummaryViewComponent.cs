using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class RoleSummaryViewComponent : ViewComponent
    {
        private RoleManager<IdentityRole> _roleManager;

        public RoleSummaryViewComponent(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public string Invoke()
        {
            return _roleManager.Roles.Count().ToString();
        }
    }
}