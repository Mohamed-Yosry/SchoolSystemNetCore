using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Domain.Shared.ApiResponse;
using SchoolSystem.Service.Contract.Services;
using SchoolSystem.ViewModels;

namespace SchoolSystem.Controllers.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = await _userService.GetAllUsersVM();
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                var exceptionResponse = CreateExceptionResponse(ex.Message);
                return StatusCode(exceptionResponse.StatusCode, exceptionResponse);
            }
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] string Id)
        {
            try
            {
                var response = await _userService.GetUserVMById(Id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                var exceptionResponse = CreateExceptionResponse(ex.Message);
                return StatusCode(exceptionResponse.StatusCode, exceptionResponse);
            }
        }
        [HttpGet]
        [Route("GetAllUsers/{RoleName}")]
        public async Task<IActionResult> GetAllStudents(string RoleName)
        {
            try
            {
                var response = await _userService.GetAllUsersVMInRole(RoleName);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                var exceptionResponse = CreateExceptionResponse(ex.Message);
                return StatusCode(exceptionResponse.StatusCode, exceptionResponse);
            }
        }
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateUser(UserViewModel userVM)
        {
            try
            {
                var response = await _userService.UpdateUserVM(userVM);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                var exceptionResponse = CreateExceptionResponse(ex.Message);
                return StatusCode(exceptionResponse.StatusCode, exceptionResponse);
            }
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            try
            {
                var reponse = await _userService.DeleteUserVM(Id);
                return StatusCode(reponse.StatusCode, reponse);
            }
            catch(Exception ex)
            {
                var exceptionResponse = CreateExceptionResponse(ex.Message);
                return StatusCode(exceptionResponse.StatusCode, exceptionResponse);
            }
        }
        [HttpPut]
        [Route("activate/{Id}")]
        public async Task<IActionResult> ActivateUser(string Id)
        {
            try
            {
                var response = await _userService.Activate(Id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                var exceptionResponse = CreateExceptionResponse(ex.Message);
                return StatusCode(exceptionResponse.StatusCode, exceptionResponse);
            }
        }
        [HttpPut]
        [Route("deactivate/{Id}")]
        public async Task<IActionResult> DeactivateUser(string Id)
        {
            try
            {
                var response = await _userService.Deactivate(Id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                var exceptionResponse = CreateExceptionResponse(ex.Message);
                return StatusCode(exceptionResponse.StatusCode, exceptionResponse);
            }
        }
        private ApiResponse<UserViewModel> CreateExceptionResponse(string ExceptionMessage)
        {
            var exceptionResponse = new ApiResponse<UserViewModel>()
            {
                Exception = ExceptionMessage,
                StatusCode = 500
            };
            return exceptionResponse;
        }
    }
}
