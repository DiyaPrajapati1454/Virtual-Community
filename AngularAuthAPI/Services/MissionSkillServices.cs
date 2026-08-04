using AngularAuthAPI.Dto;
using AngularAuthAPI.Repositories;

namespace AngularAuthAPI.Services
{
    public class MissionSkillServices
    {
        private readonly MissionSkillRepo _repo;
        public MissionSkillServices(MissionSkillRepo repo)
        {
            _repo = repo;
        }
        public List<MissionSkillResponseDto> GetMissionSkillList()
        {
            return _repo.GetMissionSkillList();
        }

        public MissionSkillResponseDto GetMissionSkillById(int id)
        {
            return _repo.GetMissionSkillById(id);
        }

        public string AddMissionSkill(AddMissionSkillReq model)
        {
            return _repo.AddMissionSkill(model);
        }

        public string UpdateMissionSkill(MissionSkillResponseDto model)
        {
            return _repo.UpdateMissionSkill(model);
        }

        public string DeleteMissionSkill(int id)
        {
            return _repo.DeleteMissionSkill(id);
        }

    }
}
