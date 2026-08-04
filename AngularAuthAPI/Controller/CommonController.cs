using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using AngularAuthAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AngularAuthAPI.Dto.ResponseResult;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController :Controller
    {
        private readonly CommonServices _services;
        public CommonController(CommonServices services)
        {
            _services = services;
        }
        ResponseResult result=new ResponseResult();
        [HttpGet]
        [Route("CountryList")]
       // [Authorize]
        public ResponseResult CountryList()
        {
            try
            {
                result.Data = _services.CountryList();
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
        [Route("CityList/{countryId}")]
       // [Authorize]
        public ResponseResult CityList(int countryId)
        {
            try
            {
                result.Data = _services.CityList(countryId);
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
        [Route("MissionCountryList")]
        public ResponseResult MissionCountryList()
        {
            try
            {
                result.Data = _services.MissionCountryList();
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
        [Route("MissionCityList")]
        public ResponseResult MissionCityList()
        {
            try
            {
                result.Data = _services.MissionCityList();
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
        [Route("MissionThemeList")]
        public ResponseResult MissionThemeList()
        {
            try
            {
                result.Data = _services.MissionThemeList();
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
        [Route("MissionSkillList")]
        public ResponseResult MissionSkillList()
        {
            try
            {
                result.Data = _services.MissionSkillList();
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
        [Route("MissionTitleList")]
        public ResponseResult MissionTitleList()
        {
            try
            {
                result.Data = _services.MissionTitleList();
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
        [Route("UploadImage")]
        public async Task<ResponseResult> UploadImage([FromForm] List<IFormFile> files)
        {
            List<string> fileList = new List<string>();
            if(files != null && files.Count>0){
                foreach ( IFormFile file in files)
                {
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadImages", "MissionImages");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    var name=Path.GetFileNameWithoutExtension(file.FileName);
                    var ext=Path.GetExtension(file.FileName);
                    var unique = name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                    var filePath=Path.Combine(uploadFolder, unique);
                    using (var stream = new FileStream(filePath,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fPath=Path.Combine("UploadImages","MissionImages", unique);
                    fileList.Add(fPath);
                }

            }
            return new ResponseResult() { Data = fileList, message = "Success", Result = ResponseStatus.Success };
        }
        [HttpGet]
        [Route("GetUserSkill/{userId}")]
        public ResponseResult GetUserSkill(int userId)
        {
            try
            {
                result.Data = _services.GetUserSkill(userId);
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
        [Route("AddUserSkill")]
        public async Task<ResponseResult> AddUserSkill(UserSkill skills)
        {
            try
            {
                result.Data = await _services.AddUserSkill(skills);
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
