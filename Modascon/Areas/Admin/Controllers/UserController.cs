using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System.Data;

namespace Modascon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IServiceManager _manager;

        public UserController(UserManager<IdentityUser> userManager, IServiceManager manager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _manager = manager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = userDto.UserName, Email = userDto.Email, PhoneNumber = userDto.PhoneNumber };
                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(userDto);
        }

        public async Task<IActionResult> Update(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(id);

            if (user is not null)
            {
                ViewBag.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
                return View(new UserDtoForUpdate
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    SelectedRoles = await _userManager.GetRolesAsync(user)
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserDtoForUpdate model, string id)
        {
            if (id != model.Id)
            {
                RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.PhoneNumber = model.PhoneNumber;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                        if (model.SelectedRoles != null)
                        {
                            await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                        }

                        return RedirectToAction("Index");
                    }
                    foreach (IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ResetPassword([FromRoute(Name = "id")] string id)
        {
            return View(new ResetPasswordDto()
            {
                UserName = id,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    var result = await _userManager.RemovePasswordAsync(user);

                    if (result.Succeeded && !string.IsNullOrEmpty(model.Password) && model.Password == model.ConfirmPassword)
                    {
                        await _userManager.AddPasswordAsync(user, model.Password);
                        ModelState.Clear();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Şifreler birbiriyle uyuşmuyor.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }


    }
}
