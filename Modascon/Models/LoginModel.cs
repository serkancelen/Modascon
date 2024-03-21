using System.ComponentModel.DataAnnotations;

namespace Modascon.Models
{
    public class LoginModel
    {
        private string _returnUrl;
        [Required(ErrorMessage = "E-mail boş bırakılamaz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre boş bırakılamaz")]  
        public string Password { get; set; }
        public string ReturnUrl
        {
            get
            {
                if (_returnUrl is null)
                {
                    return "/";
                }
                else
                {
                    return _returnUrl;
                }
            }
            set
            {
                _returnUrl = value;
            }
        }
    }
}
