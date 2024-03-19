using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ResetPasswordDto
    {
        public string UserName { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Şifre Boş Olamaz")]
        public string Password { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Onaylama Boş Olamaz")]
        public string ConfirmPassword { get; init; }
    }
}
