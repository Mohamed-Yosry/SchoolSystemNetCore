using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Service.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        [Route("GetAllRolesName")]
        public IActionResult GetAllRolesName()
        {
            var roles = _roleService.GetAllRoles();
            if (roles == null)
                return BadRequest();
            return Ok(roles);
        }
    }
}
