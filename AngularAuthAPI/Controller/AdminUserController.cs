using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using AngularAuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using static AngularAuthAPI.Dto.ResponseResult;

namespace AngularAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminUserController : Controller
    {
        private readonly AdminUserService _adminUserService;
        private readonly UserService _userService;
        public AdminUserController(AdminUserService adminUserService, UserService userService)
        {
            _adminUserService = adminUserService;
            _userService = userService;
        }
        [HttpGet]
        [Route("UserDetailList")]
        public ActionResult UserDetailList()
        {
            try
            {
                var res = _adminUserService.UserDetailsList();
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, message = "Failed to get user list" });
            }
        }
        [HttpPost]
        [Route("Add User(Admin)")]
        public async Task<ActionResult> RegisterUser(RegisterUserDetails user)
        {
            await _userService.RegisterUser(user);
            return Ok("User Added");
        }
        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult DeleteUser([FromQuery] int id)
        {
            try
            {
                var res = _adminUserService.UserDelete(id);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, message = "" });
            }
            catch(Exception ex) 
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, message = "Failed to delete user "+ex.Message });
            }
        }

    }
}
