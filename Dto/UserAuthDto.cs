﻿using System.ComponentModel.DataAnnotations;

namespace Hackathon.Dto
{
    public class UserAuthDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
