﻿using Microsoft.AspNetCore.Identity;

namespace JobzApi.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}