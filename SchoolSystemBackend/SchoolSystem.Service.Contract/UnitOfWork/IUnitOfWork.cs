using SchoolSystem.Domain.Models;
using SchoolSystem.Domain.Models.AuthenticationModels;
using SchoolSystem.Service.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseSerivce<ApplicationUser> Users { get; }
        IBaseSerivce<Course> Courses { get; }
        int Complete();
    }
}
