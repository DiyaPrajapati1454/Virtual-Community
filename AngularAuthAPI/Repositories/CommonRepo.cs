using AngularAuthAPI.Data;
using AngularAuthAPI.Models;

namespace AngularAuthAPI.Repositories
{
    public class CommonRepo
    {
        private readonly Connection conn;
        public CommonRepo(Connection conn)
        {
            this.conn = conn;
        }
        public List<DropDownResponseModel> CountryList()
        {
            var countries = conn.Country.OrderBy(c => c.CountryName).Select(c => new DropDownResponseModel(c.Id, c.CountryName)).ToList();
            return countries;
        }
        public List<DropDownResponseModel> CityList(int countryId)
        {
            var city=conn.City.Where(c=>c.CountryId==countryId).OrderBy(c=>c.CityName).Select(c=>new DropDownResponseModel(c.Id,c.CityName)).ToList();
            return city;
        }
        public List<DropDownResponseModel> MissionCountryList()
        {
            var country = conn.Missions.Select(x => new DropDownResponseModel(x.CountryId, x.Country.CountryName)).ToList();
            return country;
        }
        public List<DropDownResponseModel> MissionCityList()
        {
            var city = conn.Missions.Select(x => new DropDownResponseModel(x.CityId, x.City.CityName)).ToList();
            return city;
        }
        public  List<DropDownResponseModel> MissionThemeList()
        {
            var missionThemes=conn.MissionThemes.Where(x=>x.Status.ToLower()=="active").Select(x=>new DropDownResponseModel(x.Id,x.ThemeName)).ToList();
            return missionThemes;
        }
        public List<DropDownResponseModel> MissionSkillList()
        {
            var missionSkill = conn.MissionSkills.Where(x => x.Status.ToLower() == "active").Select(x => new DropDownResponseModel(x.Id, x.SkillName)).ToList();
            return missionSkill;
        }
        public List<DropDownResponseModel> MissionTitleList()
        {
            var mission = conn.Missions.Where(x => !x.isDeleted).Select(x => new DropDownResponseModel(x.Id, x.MissionTitle)).ToList();
            return mission;
        }
        public List<DropDownResponseModel> GetUserSkill(int userId)
        {
            var userSkill = conn.UserSkills
                .Where(m => m.UserId == userId)
                .Select(m => new DropDownResponseModel(m.Id, m.Skill))
                .ToList();

            return userSkill;
        }

        public async Task<bool> AddUserSkill(UserSkill skills)
        {
            try
            {
                await conn.UserSkills.AddAsync(skills);
                await conn.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
