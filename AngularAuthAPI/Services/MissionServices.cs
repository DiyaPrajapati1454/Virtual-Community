using AngularAuthAPI.Controllers;
using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using AngularAuthAPI.Repositories;

namespace AngularAuthAPI.Services
{
    public class MissionServices
    {
        private readonly MissionRepo _repo;
        private readonly MissionSkillRepo _skillRepo;
        public MissionServices(MissionRepo repo,MissionSkillRepo skillRepo)
        {
            _repo = repo;
            _skillRepo = skillRepo;
        }
        public Task<List<Missions>> GetMissionList()
        {
            return _repo.GetMissionList();
        }

        public Task<string> AddMission(AddMissionReq model)
        {
            return _repo.AddMission(model);
        }
        public async Task<IList<MissionResponseDto>> ClientSideMissionList(int userId)
        {
            var mission=await _repo.ClientSideMissionList();
            return mission.Select(m => new MissionResponseDto()
            {
                Id=m.Id,
                EndDate=m.EndDate,
                StartDate=m.StartDate,
                MissionDescription=m.MissionDescription,
                MissionImages=m.MissionImages,
                MissionTitle=m.MissionTitle,
                TotalSheets=m.TotalSheets,
                RegistrationDeadLine=m.RegistrationDeadLine,
                CityId=m.CityId,
                CityName=m.City.CityName,
                CountryId=m.CountryId,
                CountryName=m.Country.CountryName,
                MissionSkillId=m.MissionSkillId,
                MissionSkillName=_skillRepo.GetMissionSkillById(m.MissionSkillId).SkillName,
                MissionThemeId=m.MissionThemeId,
                MissionThemeName=m.MissionTheme.ThemeName,
                MissionApplyStatus = m.MissionApplications.Any(m => m.UserId == userId) ? "Applied" : "Apply",
                MissionApproveStatus = m.MissionApplications.Any(m => m.UserId == userId && m.Status == true) ? "Approved" : "Applied",
                MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available"
            }).ToList();
        }
        public async Task<bool> ApplyMission(AddMissionApplicationReq model)
        {
            return await _repo.ApplyMission(model);
        }
        public List<MissionApplication> GetMissionApplicationList()
        {
            return _repo.GetMissionApplicationList();
        }

        public async Task<bool> MissionApplicationApprove(UpdateMissionApplicationReq missionApplication)
        {
            return await _repo.MissionApplicationApprove(missionApplication);
        }
    }
}
