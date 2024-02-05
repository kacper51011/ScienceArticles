using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.Register
{
    public class RegisterRequestDto
    {
        [Required]
        public string UserName {  get; set; }

        [PasswordPropertyText]
        [Required]
        public string Password { get; set; }

        [PasswordPropertyText]
        [Required]
        public string ConfirmPassword {  get; set; }
    }
}
