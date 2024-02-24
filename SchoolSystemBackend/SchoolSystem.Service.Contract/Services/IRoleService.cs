using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.Services
{
    public interface IRoleService
    {
        List<IdentityRole> GetAllRoles();
    }
}
