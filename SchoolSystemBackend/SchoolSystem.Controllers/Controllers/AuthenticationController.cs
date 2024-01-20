using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Domain.Models.AuthenticationModels;
using SchoolSystem.Service.Contract.DTO;
using SchoolSystem.Service.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// register new user by register model
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var result = await _authService.RegisterUser(registerModel);
            if (!result.IsAuthenticated)
                return BadRequest(result);
            SetRefreshtokenInCookies(result.RefreshToken, result.RefreshTokenExpireation);
            return Ok(result);
        }
        /// <summary>
        /// login by email and password
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _authService.GetTokenAsync(loginModel);
            if (!result.IsAuthenticated)
                return BadRequest(result);
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshtokenInCookies(result.RefreshToken, result.RefreshTokenExpireation);
            return Ok(result);
        }
        /// <summary>
        /// get new refresh token and access token and revoke old one
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RefreshToken")]        
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (!result.IsAuthenticated)
                return BadRequest(result);
            SetRefreshtokenInCookies(result.RefreshToken, result.RefreshTokenExpireation);
            return Ok(result);
        }
        /// <summary>
        /// this endpoint revokes valid refresh tokens
        /// </summary>
        /// <param name="revokeTokenDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDTO revokeTokenDTO)
        {
            var token = revokeTokenDTO.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required");
            var result = await _authService.RevokeTokenAsync(token);
            if(!result)
                return BadRequest("Token is invalid");
            return Ok();
        }

        /// <summary>
        /// set the refresh token in cookies and set cooki options
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="expires"></param>
        private void SetRefreshtokenInCookies(string refreshToken, DateTime expires)
        {
            var cookies = new CookieOptions
            {
                Expires = expires.ToLocalTime(),
                HttpOnly = true
            };
            Response.Cookies.Append("refreshToken", refreshToken,cookies);
        }
    }
}
