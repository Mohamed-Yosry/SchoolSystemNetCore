using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Models.AuthenticationModels
{
    [Owned]
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public DateTime ExpiriesOn { get; set; }
        public bool IsExpired => DateTime.Now >= ExpiriesOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedAt { get; set; }
        public bool IsActive => RevokedAt == null && !IsExpired;
    }
}
