﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacticeService.DTOs.UserDto
{
    public class SignupRequest
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Username {  get; set; }
        public string Email { get; set; }
        public string Password {  get; set; }
        public string ConfirmPassword { get; set; }

    }
}
