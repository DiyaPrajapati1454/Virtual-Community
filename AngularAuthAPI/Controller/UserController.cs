using AngularAuthAPI.Dto;
using AngularAuthAPI.Helper;
using AngularAuthAPI.Models;
using AngularAuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static AngularAuthAPI.Dto.ResponseResult;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly JwtHelper jwtHelper;
        private readonly UserService _userService;
        public UserController(UserService userService, JwtHelper jwtHelper)
        {
           _userService = userService;
            this.jwtHelper = jwtHelper;
        }
        [HttpPost]
        [Route("Add User")]
        public async Task<ActionResult> AddUSer(UserDetailReq user)
        {
            await _userService.AddUser(user);
            return Ok("User Added");
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] LoginReqDto dto)
        {
            try
            {
                var user = _userService.Login(dto.Email, dto.Password);
                if (user == null)
                {
                    return NotFound("Check Email or Password");
                }
                else
                {
                    var token = jwtHelper.GetJwtToken(user);
                    return Ok(new LoginResDto() {Id=user.Id, Email = user.Email, Name = user.First_Name, Role = user.type, Token = token });
                }
            }
            catch (Exception ex) {
                return StatusCode(500,"Login Exception"+ex.ToString());
            }
        }
        [HttpGet]
        [Route("GetUserById")]
        public ActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to Fetch");
            }
        }
        [HttpGet]
        [Route("GetUserDetails")]
        public ActionResult GetUserDetails(int id)
        {
            try
            {
                var user = _userService.GetUserDetails(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to Fetch");
            }
        }
        [HttpPost]
        [Route("LoginUserProfileUpdate")]
        public async Task<ActionResult> LoginUserProfileUpdate([FromBody] UserDetailReq requestModel)
        {
            try
            {
                var res = await _userService.LoginUserProfileUpdate(requestModel);
                return Ok(new ResponseResult() { Data = "Data Updated!", Result = ResponseStatus.Success, message = "" });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, message = "Failed to add user. "+ex.ToString() });
            }
        }
    }
}
