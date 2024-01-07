using Microsoft.AspNetCore.Identity;
using SchoolSystem.Domain.Models.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.Services
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateJwtToken(IdentityUser user);
        Task<AuthModel> RegisterUser(RegisterModel registerModel);
        Task<AuthModel> GetTokenAsync(LoginModel loginModel);
    }
}
