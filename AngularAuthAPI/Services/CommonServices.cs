using AngularAuthAPI.Models;
using AngularAuthAPI.Repositories;

namespace AngularAuthAPI.Services
{
    public class CommonServices
    {
        private readonly CommonRepo _repo;
        public CommonServices(CommonRepo repo)
        {
            _repo = repo;
        }
        public List<DropDownResponseModel> CountryList()
        {
            return _repo.CountryList();
        }

        public List<DropDownResponseModel> CityList(int countryId)
        {
            return _repo.CityList(countryId);
        }

        public List<DropDownResponseModel> MissionCountryList()
        {
            return _repo.MissionCountryList();
        }

        public List<DropDownResponseModel> MissionCityList()
        {
            return _repo.MissionCityList();
        }

        public List<DropDownResponseModel> MissionThemeList()
        {
            return _repo.MissionThemeList();
        }

        public List<DropDownResponseModel> MissionSkillList()
        {
            return _repo.MissionSkillList();
        }

        public List<DropDownResponseModel> MissionTitleList()
        {
            return _repo.MissionTitleList();
        }
        public List<DropDownResponseModel> GetUserSkill(int userId)
        {
            return _repo.GetUserSkill(userId);
        }

        public async Task<bool> AddUserSkill(UserSkill skills)
        {
            return await _repo.AddUserSkill(skills);
        }

    }
}
