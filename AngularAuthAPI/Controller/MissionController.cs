using AngularAuthAPI.Dto;
using AngularAuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using static AngularAuthAPI.Dto.ResponseResult;

namespace AngularAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionController : Controller
    {
        private readonly MissionServices _services;
        public MissionController(MissionServices services)
        {
            _services = services;
        }
        [HttpGet]
        [Route("MissionList")]
        public async Task<ResponseResult> MissionList()
        {
            var mission = await _services.GetMissionList();
            return new ResponseResult() { Data = mission, message = "", Result = ResponseStatus.Success };
        }

        [HttpPost]
        [Route("AddMission")]
        public ActionResult AddMission(AddMissionReq model)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                var data = _services.AddMission(model);
                result.Data = data;
                result.message = "Success";
                result.Result = ResponseStatus.Success;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.message = ex.Message;
                result.Result = ResponseStatus.Error;
                return BadRequest(result);
            }
        }
        [HttpGet]
        [Route("MissionApplicationList")]
        public IActionResult MissionApplicationList()
        {
            var response = _services.GetMissionApplicationList();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, message = "" });
        }

        [HttpPost]
        [Route("MissionApplicationApprove")]
        public async Task<IActionResult> MissionApplicationApprove(UpdateMissionApplicationReq missionApp)
        {
            try
            {
                var ret = await _services.MissionApplicationApprove(missionApp);
                return Ok(new ResponseResult() { Data = ret, message = string.Empty, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, message = ex.Message, Result = ResponseStatus.Error });
            }
        }
    }
}
