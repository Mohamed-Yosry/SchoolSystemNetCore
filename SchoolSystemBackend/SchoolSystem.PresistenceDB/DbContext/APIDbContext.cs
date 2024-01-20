using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Domain.Models.AuthenticationModels;

namespace SchoolSystem.PresistenceDB.DbContext
{
    public class APIDbContext : IdentityDbContext<ApplicationUser>
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {

        }
    }
}
