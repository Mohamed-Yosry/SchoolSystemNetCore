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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly IMapper _mapper;
        public AuthService(UserManager<ApplicationUser> userManager,
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
        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
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
            var authModel = new AuthModel();
            // check if the email and password is correct
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
                return new AuthModel { Message = "Incorrect Email or Password" };
            var checkPassword = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!checkPassword)
                return new AuthModel { Message = "Incorrect Email or Password" };
            
            // genreate the token
            var token = await CreateJwtToken(user);
            authModel =  new AuthModel
            {
                Email = user.Email,
                UserName = user.UserName,
                Roles = new List<string>(),
                ExpiersOn = token.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            // genreate refesh token
            if (user.RefreshTokens.Any(token => token.IsActive))
            {
                var refreshToken = user.RefreshTokens.FirstOrDefault(token => token.IsActive);
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpireation = refreshToken.ExpiriesOn;
            }
            else
            {
                var refreshToken = GetRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpireation = refreshToken.ExpiriesOn;
                var RefreshTokens = user.RefreshTokens.ToList();
                RefreshTokens.Add(refreshToken);
                user.RefreshTokens = RefreshTokens;
                await _userManager.UpdateAsync(user);
            }

            return authModel;
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
            var user = _mapper.Map<ApplicationUser>(registerModel);
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
        /// <summary>
        /// get new access token using refresh token and create new refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModel();
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            if(user == null)
            {
                authModel.IsAuthenticated = false;
                authModel.Message = "Invalid Token";
                return authModel;
            }

            var userToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!userToken.IsActive)
            {
                authModel.IsAuthenticated = false;
                authModel.Message = "Inactive Token";
                return authModel;
            }

            userToken.RevokedAt = DateTime.UtcNow;
            var newRefreshToken = GetRefreshToken();
            var RefreshTokens = user.RefreshTokens.ToList();
            RefreshTokens.Add(newRefreshToken);
            user.RefreshTokens = RefreshTokens;
            await _userManager.UpdateAsync(user);

            var newToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(newToken);
            authModel.Roles = await _userManager.GetRolesAsync(user);
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpireation = newRefreshToken.ExpiriesOn;

            return authModel;
        }
        /// <summary>
        /// this method revokes refresh tokens
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
                return false;

            var userToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!userToken.IsActive)
                return false;


            userToken.RevokedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return true;
        }

        /// <summary>
        /// Genereate new Refresh tokem model
        /// </summary>
        /// <returns></returns>
        private RefreshTokenModel GetRefreshToken()
        {
            var randomNumber = new byte[32];
            var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshTokenModel
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiriesOn = DateTime.Now.AddDays(10),
                CreatedOn = DateTime.Now
            };
        }
    }
}
