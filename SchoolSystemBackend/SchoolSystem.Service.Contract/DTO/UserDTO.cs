using SchoolSystem.Domain.Models.AuthenticationModels;
using SchoolSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<CourseDTO> Courses { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
    }
}
