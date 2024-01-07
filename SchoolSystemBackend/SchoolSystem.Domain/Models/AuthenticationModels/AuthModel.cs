using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Models.AuthenticationModels
{
    public class AuthModel
    {
        public string Message { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime ExpiersOn { get; set; }
        public IList<string> Roles { get; set; }
        //public DateTime RefreshTokenExpireation { get; set; }
        //[JsonIgnore]
        //public string? RefreshToken { get; set; }
    }
}
