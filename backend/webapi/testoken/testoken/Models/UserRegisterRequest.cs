﻿using System.ComponentModel.DataAnnotations;

namespace testoken.Models
{
    public class UserRegisterRequest
    {

        public string Username { get; set; } = string.Empty;

        [Required , EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required,Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
