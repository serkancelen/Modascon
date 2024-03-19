using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modascon.Models;

namespace Modascon.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IEmailSender _emailSender;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string returnUrl = "/")
        {
            return View(new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(model.Name);
                if (user is not null)
                {
                    await _signInManager.SignOutAsync();
                    if(!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınızı onaylanma işlemini mail üzerinden gerçekleştiriniz");
                        return View(model);
                    }

                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        return Redirect(model.ReturnUrl ?? "/");
                    }

                    ModelState.AddModelError("Error", "Hatalı Kullanıcı adı ya da şifre");
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var roleResult = await _userManager
                    .AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "Auth", new {user.Id, token});

                    await _emailSender.SendEmailAsync(user.Email, "Hesap Onayı", $"Lütfen Email hesabınızı onaylamak için linke <a href='https://localhost:7270{url}'>tıklayınız.</a>"); 

                    TempData["message"] = "Email hesabınızdaki onay mailine tıklayınız.";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
            if (Id == null || token == null)
            {
                TempData["message"] = "Geçersiz Token Bilgisi";
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["message"] = "Hesabınız Onaylandı";
                    return RedirectToAction("Login","Auth");
                }
            }
            TempData["message"] = "Kullanıcı Bulunamadı";
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public async Task<IActionResult> ForgotPasswordWithEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Auth", new { user.Id, token });
            await _emailSender.SendEmailAsync(Email,"Parola Sıfırlama", $"Parolanızı yenilemek için linke <a href='https://localhost:7270{url}'>tıklayınız</a>.");

            TempData["message"] = "Email adresinize gönderilen link ile şifrenizi sıfırlayabilirsiniz";

            return RedirectToAction("Login");
        }
        public IActionResult ResetPassword(string Id, string token)
        {
            if (Id == null || token == null)
            {
                return RedirectToAction("Login");
            }
            var model = new ForgotPasswordDto { Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ForgotPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    TempData["message"] = "Bu Mail Adresiyle Eşleşen Kullanıcı Yok";
                    return RedirectToAction("Login");
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    TempData["message"] = "Şifreniz Değiştirildi";
                    return RedirectToAction("Login");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }
    }
}
