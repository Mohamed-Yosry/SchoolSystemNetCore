using SchoolSystem.Domain.Shared.ApiResponse;
using SchoolSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.Services
{
    public interface IUserService
    {
        Task<ApiResponse<List<UserViewModel>>> GetAllUsersVM();
        Task<ApiResponse<List<UserViewModel>>> GetAllUsersVMInRole(string roleName);
        Task<ApiResponse<UserViewModel>> GetUserVMById(string Id);
        Task<ApiResponse<UserViewModel>> UpdateUserVM(UserViewModel userViewModel);
        Task<ApiResponse<UserViewModel>> DeleteUserVM(string Id);
        Task<ApiResponse<UserViewModel>> Activate(string Id);
        Task<ApiResponse<UserViewModel>> Deactivate(string Id);
    }
}
