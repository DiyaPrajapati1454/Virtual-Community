using AngularAuthAPI.Dto;
using AngularAuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using static AngularAuthAPI.Dto.ResponseResult;

namespace AngularAuthAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClientMissionController : Controller
    {
        private readonly MissionServices _services;
        public ClientMissionController(MissionServices services)
        {
            _services = services;
        }
        [HttpGet]
        [Route("ClientSideMissionList")]
        public async Task<IActionResult> ClientSideMissionList(int userId)
        {
            try
            {
                var missions = await _services.ClientSideMissionList(userId);
                return Ok(new ResponseResult() { Data = missions, message = string.Empty, Result = ResponseStatus.Success });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, message = "Error in fetching missions for user.", Result = ResponseStatus.Error });

            }
        }
        [HttpPost]
        [Route("ApplyMission")]
        public async Task<IActionResult> ApplyMission(AddMissionApplicationReq model)
        {
            try
            {
                var ret = await _services.ApplyMission(model);
                return Ok(new ResponseResult() { Data = ret, message = string.Empty, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, message = ex.Message, Result = ResponseStatus.Error });
            }
        }
    }
}
