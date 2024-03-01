using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolSystem.Domain.Models.AuthenticationModels;
using SchoolSystem.Domain.Shared.ApiResponse;
using SchoolSystem.Service.Contract;
using SchoolSystem.Service.Contract.Services;
using SchoolSystem.ViewModels;

namespace SchoolSystem.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserService(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<UserViewModel>>> GetAllUsersVM()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            if(users == null)
                return new ApiResponse<List<UserViewModel>>() { Message = "Users Doesn't Exist", StatusCode = 400 };
            // get users
            var usersVM = _mapper.Map<List<UserViewModel>>(users);
            return new ApiResponse<List<UserViewModel>>() { Message = "Get all users succeded", Data = usersVM, StatusCode = 200 };
        }
        public async Task<ApiResponse<List<UserViewModel>>> GetAllUsersVMInRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if(!roleExist)
                return new ApiResponse<List<UserViewModel>>() { Message = "Role Doesn't Exist", StatusCode = 400 };
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            if (users == null)
                return new ApiResponse<List<UserViewModel>>() { Message = "Users Doesn't Exist", StatusCode = 400 };
            // get users
            var usersVM = _mapper.Map<List<UserViewModel>>(users);
            return new ApiResponse<List<UserViewModel>>() { Message = "Get all users succeded", Data = usersVM, StatusCode = 200 };
        }
        public async Task<ApiResponse<UserViewModel>> GetUserVMById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return new ApiResponse<UserViewModel>() { Message = "User Doesn't Exist", StatusCode = 400 };
            // get user
            var userVM = _mapper.Map<UserViewModel>(user);
            return new ApiResponse<UserViewModel>() { Message = "Get user succeded", Data = userVM, StatusCode = 200 };
        }
        public async Task<ApiResponse<UserViewModel>> UpdateUserVM(UserViewModel userViewModel)
        {
            // check if user exist
            var user = await _userManager.FindByIdAsync(userViewModel.Id);
            if (user == null)
                return new ApiResponse<UserViewModel>() { Message = "User Doesn't Exist", StatusCode = 400 };
            // check if updated email exist
            var emailUserExist = await _userManager.FindByEmailAsync(userViewModel.Email);
            if (emailUserExist != null && emailUserExist.Id != userViewModel.Id)
                return new ApiResponse<UserViewModel>() { Message = "Email is Exist", StatusCode = 400 };
            // check if updated username exist
            var userNameExist = await _userManager.FindByNameAsync(userViewModel.UserName);
            if (userNameExist != null && userNameExist.Id != userViewModel.Id)
                return new ApiResponse<UserViewModel>() { Message = "UserName is Exist", StatusCode = 400 };
            // update user
            user.UserName = userViewModel.UserName;
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            // courses when finisheing courses controller and service
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var updatedUserVM = _mapper.Map<UserViewModel>(user);
                return new ApiResponse<UserViewModel>() { Message = "User update succeded", Data = updatedUserVM, StatusCode = 200 };
            }
            return new ApiResponse<UserViewModel>() { Message = "User update failed", StatusCode = 400 };
        }
        public async Task<ApiResponse<UserViewModel>> DeleteUserVM(string Id)
        {
            // check if user exist
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return new ApiResponse<UserViewModel>() { Message = "User Doesn't Exist", StatusCode = 400 };
            // delete user
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ApiResponse<UserViewModel>() { Message = "User delete succeded", StatusCode = 204 };
            }
            return new ApiResponse<UserViewModel>() { Message = "User delete failed", StatusCode = 400 };
        }
        public async Task<ApiResponse<UserViewModel>> Activate(string Id)
        {
            // check if user exist
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return new ApiResponse<UserViewModel>() { Message = "User Doesn't Exist", StatusCode = 400 };
            // activate user
            if (user.IsActive)
                return new ApiResponse<UserViewModel>() { Message = "User is already active succeded", StatusCode = 200 };
            user.IsActive = true;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return new ApiResponse<UserViewModel>() { Message = "User activate succeded", StatusCode = 200 };
            return new ApiResponse<UserViewModel>() { Message = "User activate failed", StatusCode = 400 };
        }
        public async Task<ApiResponse<UserViewModel>> Deactivate(string Id)
        {
            // check if user exist
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return new ApiResponse<UserViewModel>() { Message = "User Doesn't Exist", StatusCode = 400 };
            // deactivate user
            if(!user.IsActive)
                return new ApiResponse<UserViewModel>() { Message = "User is already deactive succeded", StatusCode = 200 };
            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return new ApiResponse<UserViewModel>() { Message = "User deactivate succeded", StatusCode = 200 };
            return new ApiResponse<UserViewModel>() { Message = "User deactivate failed", StatusCode = 400 };
        }
    }
}
