﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.Login
{
    public class LoginRequestDto
    {
        [Required]
        public string Login {  get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
