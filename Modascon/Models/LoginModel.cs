using System.ComponentModel.DataAnnotations;

namespace Modascon.Models
{
    public class LoginModel
    {
        private string _returnUrl;
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz")]
        public string Name { get; set; }
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
