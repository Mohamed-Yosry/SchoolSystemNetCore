using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.DTO
{
    public class RoleDTO
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        List<UserDTO> users { get; set; }
    }
}
