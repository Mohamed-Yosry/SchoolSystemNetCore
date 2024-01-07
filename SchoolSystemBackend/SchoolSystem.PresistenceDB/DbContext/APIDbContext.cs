using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.PresistenceDB.DbContext
{
    public class APIDbContext : IdentityDbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
