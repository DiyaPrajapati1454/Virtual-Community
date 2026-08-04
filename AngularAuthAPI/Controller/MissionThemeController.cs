using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using AngularAuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using static AngularAuthAPI.Dto.ResponseResult;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionThemeController : Controller
    {
        private readonly MissionThemeServices missionThemeServices;
        public MissionThemeController(MissionThemeServices missionThemeServices) { 
            this.missionThemeServices = missionThemeServices;
        }
        [HttpGet]
        [Route("GetMissionThemeList")]
        public async Task<IActionResult> GetAllMissionTheme()
        {
            try
            {
                var res = await missionThemeServices.GetAllMissionTheme();
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, message = "Failed to get mission theme" });
            }
        }

        [HttpPost]
        [Route("AddMissionTheme")]
        public async Task<IActionResult> AddMissionTheme(AddMissionThemeDto missionThemeViewModel)
        {
            try
            {
                var res = await missionThemeServices.AddMissionTheme(missionThemeViewModel);
                return Ok(new ResponseResult() { Data = "Added Mission theme.", Result = ResponseStatus.Success, message = "" });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, message = "Failed to add mission theme " });
            }
        }

        [HttpGet]
        [Route("GetMissionThemeById/{id:int}")]
        public async Task<IActionResult> GetMissionThemeById(int id)
        {
            var res = await missionThemeServices.GetMissionThemeById(id);

            if (res == null)
                return NotFound(new ResponseResult() { Data = "Not Found", Result = ResponseStatus.Error,message = "" });

            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, message = "" });
        }

        [HttpPost]
        [Route("UpdateMissionTheme")]
        public async Task<IActionResult> UpdateMissionTheme(MissionThemeDto missionThemeViewModel)
        {
            var res = await missionThemeServices.UpdateMissionTheme(missionThemeViewModel);

            if (!res)
                return NotFound(new ResponseResult() { Data = "Not Found", Result = ResponseStatus.Error, message = "" });

            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, message = "" });
        }

        [HttpDelete]
        [Route("DeleteMissionTheme{id:int}")]
        public async Task<IActionResult> DeleteMissionTheme(int id)
        {
            var res = await missionThemeServices.DeleteMissionTheme(id);

            if (!res)
                return NotFound(new ResponseResult() { Data = "Not Found", Result = ResponseStatus.Error,message = "" });

            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, message = "Record Delete Successfully" });
        }
    }
}
