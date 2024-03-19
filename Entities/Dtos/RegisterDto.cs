using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "E-Mail adı boş olamaz")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Şifre adı boş olamaz")]
        public string Password { get; init; }

    }
}
