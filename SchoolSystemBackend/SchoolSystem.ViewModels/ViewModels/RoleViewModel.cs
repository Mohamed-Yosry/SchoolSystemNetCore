using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.ViewModels
{
    public class RoleViewModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        List<UserViewModel> users { get; set; }
    }
}
