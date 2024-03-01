﻿using Microsoft.AspNetCore.Identity;

namespace SchoolSystem.Domain.Models.AuthenticationModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<RefreshTokenModel>? RefreshTokens { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
