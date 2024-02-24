using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Service.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }
    }
}
