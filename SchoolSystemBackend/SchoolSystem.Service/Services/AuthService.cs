using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolSystem.Domain.Models.AuthenticationModels;
using SchoolSystem.Service.Contract.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly IMapper _mapper;
        public AuthService(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWT> jwt,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _mapper = mapper;
        }
        /// <summary>
        /// this function generate new token based on the user claims and defined jwt model
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<JwtSecurityToken> CreateJwtToken(IdentityUser user)
        {
            // generate the claims for the token
            var userManagerClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesClaim = new List<Claim>();

            foreach (var role in userRoles)
                rolesClaim.Add(new Claim("role", role));

            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }.Union(userManagerClaims)
            .Union(rolesClaim);

            // get the security key for the token
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // generate the token
            var token = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                expires: DateTime.Now.AddHours(_jwt.DurationInHours)
                );
            return token;
        }
        /// <summary>
        /// get the token based on email and password
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<AuthModel> GetTokenAsync(LoginModel loginModel)
        {
            // check if the email and password is correct
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
                return new AuthModel { Message = "Incorrect Email or Password" };
            var checkPassword = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!checkPassword)
                return new AuthModel { Message = "Incorrect Email or Password" };
            
            // genreate the token
            var token = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                UserName = user.UserName,
                Roles = new List<string>(),
                ExpiersOn = token.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }
        /// <summary>
        /// Register new user in identity and return Authmodel contains token
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public async Task<AuthModel> RegisterUser(RegisterModel registerModel)
        {
            // check email and username existence
            var usernameExist = await _userManager.FindByNameAsync(registerModel.UserName);
            if (usernameExist != null)
                return new AuthModel { Message = "Username Exist" };
            var emailExist = await _userManager.FindByEmailAsync(registerModel.Email);
            if (emailExist != null)
                return new AuthModel { Message = "Email Exist" };
            // create user
            var user = _mapper.Map<IdentityUser>(registerModel);
            var result = await _userManager.CreateAsync(user,registerModel.Password);

            if (!result.Succeeded)
            {
                var error = string.Empty;
                foreach(var err in result.Errors)
                    error += err.Description;
                return new AuthModel { Message = error };
            }

            // create the jwt token
            var token = await CreateJwtToken(user);
            return new AuthModel
            {
                Message = "User Created",
                Email = user.Email,
                UserName = user.UserName,
                Roles = new List<string>(),
                ExpiersOn = token.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }
    }
}
