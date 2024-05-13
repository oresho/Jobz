﻿using System.ComponentModel.DataAnnotations;

namespace JobzApi.Models.Dtos.Requests
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
    }
}
