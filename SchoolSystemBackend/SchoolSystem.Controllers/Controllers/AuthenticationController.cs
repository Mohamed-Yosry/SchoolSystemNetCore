using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Domain.Models.AuthenticationModels;
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
            return Ok(result);
        }
    }
}
