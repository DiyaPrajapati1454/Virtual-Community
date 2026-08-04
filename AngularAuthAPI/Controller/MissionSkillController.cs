using AngularAuthAPI.Dto;
using AngularAuthAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AngularAuthAPI.Dto.ResponseResult;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionSkillController : Controller
    {
        
        private readonly MissionSkillServices _services;
        ResponseResult result = new ResponseResult();
        public MissionSkillController(MissionSkillServices services)
        {
            _services = services;
        }
        [HttpGet]
        [Route("GetMissionSkillList")]
       // [Authorize]
        public ResponseResult GetMissionSkillList()
        {
            try
            {
                result.Data = _services.GetMissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("GetMissionSkillById/{id}")]
       // [Authorize]
        public ResponseResult GetMissionSkillById(int id)
        {
            try
            {
                result.Data = _services.GetMissionSkillById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.message = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("AddMissionSkill")]
       // [Authorize]
        public ResponseResult AddMissionSkill(AddMissionSkillReq model)
        {
            try
            {
                result.Data = _services.AddMissionSkill(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.message = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("UpdateMissionSkill")]
    //    [Authorize]
        public ResponseResult UpdateMissionSkill(MissionSkillResponseDto model)
        {
            try
            {
                result.Data = _services.UpdateMissionSkill(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.message = ex.Message;
            }
            return result;
        }
        [HttpDelete]
        [Route("DeleteMissionSkill/{id}")]
    //    [Authorize]
        public ResponseResult DeleteMissionSkill(int id)
        {
            try
            {
                result.Data = _services.DeleteMissionSkill(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.message = ex.Message;
            }
            return result;
        }
    }
}
