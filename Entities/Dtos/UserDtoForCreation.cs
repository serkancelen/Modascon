using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record UserDtoForCreation : UserDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Şifre Boş Olamaz")]
        public string Password { get; init; }
        //[Required]
        //[DataType(DataType.Password)]
        //[Compare(nameof(Password), ErrorMessage ="Parola Eşleşmiyor")]
        //public string ConfirmPassword { get; init; }
    }
}
