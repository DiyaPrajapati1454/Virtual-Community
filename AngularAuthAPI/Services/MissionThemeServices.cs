using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using AngularAuthAPI.Repositories;

namespace AngularAuthAPI.Services
{
    public class MissionThemeServices
    {
        private readonly MissionThemeRepo missionThemeRepo;
        public MissionThemeServices(MissionThemeRepo missionThemeRepo)
        {
           this.missionThemeRepo = missionThemeRepo;
        }
        public Task<bool> AddMissionTheme(AddMissionThemeDto model)
        {
            MissionTheme missionTheme = new MissionTheme()
            {
                Status = model.Status,
                ThemeName = model.ThemeName,
            };
            return missionThemeRepo.AddMissionTheme(model);
        }
        public Task<bool> DeleteMissionTheme(int missionThemeId)
        {
            return missionThemeRepo.DeleteMissionTheme(missionThemeId);
        }
        public Task<List<MissionThemeDto>> GetAllMissionTheme()
        {
            return missionThemeRepo.GetAllMissionTheme();
        }
        public Task<MissionThemeDto?> GetMissionThemeById(int missionThemeId)
        {
            return missionThemeRepo.GetMissionThemeById(missionThemeId);
        }

        public Task<bool> UpdateMissionTheme(MissionThemeDto model)
        {
            MissionTheme missionTheme = new MissionTheme()
            {
                Id = model.Id,
                Status = model.Status,
                ThemeName = model.ThemeName,
            };
            return missionThemeRepo.UpdateMissionTheme(model);
        }


    }
}
