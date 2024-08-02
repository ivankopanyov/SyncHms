﻿using System.ComponentModel.DataAnnotations;

namespace SyncHms.Server.Dto;

public class Login
{
    [Required]
    [RegularExpression(@"^[A-Za-z][A-Za-z0-9-_]{3,49}$")]
    public string Username { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Password { get; set; }
}
