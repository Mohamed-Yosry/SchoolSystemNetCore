using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Domain.Models.AuthenticationModels;

namespace SchoolSystem.Domain.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required,MaxLength(100)]
        public string Description { get; set; }
        public int Mark { get; set; }
        public List<ApplicationUser> Subscribers { get; set; }
    }
}
