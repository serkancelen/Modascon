using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserDtoUpdate
    {
        public string Id { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Olamaz")]
        public string? UserName { get; init; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "E-Mail Boş Olamaz")]
        public string? Email { get; init; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; init; }
    }
}
