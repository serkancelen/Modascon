using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record UserDto
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Kullanıcı Adı Boş Olamaz")]
        public string UserName { get; init; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "E-Mail Boş Olamaz")]
        public string Email { get; init; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; init; }
        public HashSet<string> Roles { get; set; } = new HashSet<string>();
    }
}
