using SchoolSystem.Domain.Models;
using SchoolSystem.Domain.Models.AuthenticationModels;
using SchoolSystem.PresistenceDB.DbContext;
using SchoolSystem.Service.Contract;
using SchoolSystem.Service.Contract.Services;
using SchoolSystem.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APIDbContext _context;
        private BaseService<ApplicationUser> _userService;
        private BaseService<Course> _courseService;
        public UnitOfWork(
            APIDbContext context) => _context = context;
        public IBaseSerivce<ApplicationUser> Users { 
            get
            {
                if (_userService == null)
                    _userService = new BaseService<ApplicationUser>(_context);
                return _userService;
            }
            private set { }
        }

        public IBaseSerivce<Course> Courses
        {
            get
            {
                if (_courseService == null)
                    _courseService = new BaseService<Course>(_context);
                return _courseService;
            }
            private set { }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
