using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Models.AuthenticationModels
{
    public class LoginModel
    {
        [Required, StringLength(250)]
        public string Email { get; set; }
        [Required, StringLength(250)]
        public string Password { get; set; }
    }
}
