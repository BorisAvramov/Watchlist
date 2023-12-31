﻿using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(60)]
        [MinLength(10)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;


        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; } = null!;

    }
}
