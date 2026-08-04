using AngularAuthAPI.Data;
using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Repositories
{
    public class MissionSkillRepo
    {
        private readonly Connection conn;
        public MissionSkillRepo(Connection conn)
        {
            this.conn = conn;
        }
        public List<MissionSkillResponseDto> GetMissionSkillList()
        {
            var missionSkill = conn.MissionSkills
                .Where(ms => !ms.isDeleted)
                .Select(ms => new MissionSkillResponseDto()
                {
                    ID = ms.Id,
                    SkillName = ms.SkillName,
                    Status = ms.Status
                })
                .ToList();

            return missionSkill;
        }

        public MissionSkillResponseDto GetMissionSkillById(int id)
        {
            var missionSkillDetail = conn.MissionSkills
                .Where(ms => ms.Id == id && !ms.isDeleted)
                .Select(ms => new MissionSkillResponseDto()
                {
                    ID = ms.Id,
                    SkillName = ms.SkillName,
                    Status = ms.Status
                })
                .FirstOrDefault() ?? throw new Exception("Mission skill not found.");

            return missionSkillDetail;
        }

        public string AddMissionSkill(AddMissionSkillReq model)
        {
            var skillExist = conn.MissionSkills.Any(ms => ms.SkillName.ToLower() == model.SkillName.ToLower() && !ms.isDeleted);

            if (skillExist) throw new Exception("Skill Name Already Exist.");

            var newSkill = new MissionSkill()
            {
                SkillName = model.SkillName,
                Status = model.Status,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = null,
                isDeleted = false
            };

            conn.MissionSkills.Add(newSkill);
            conn.SaveChanges();

            return "Saved Skill Successfully..";
        }

        public string UpdateMissionSkill(MissionSkillResponseDto model)
        {
            var skillToUpdate = conn.MissionSkills.FirstOrDefault(ms => ms.Id == model.ID && !ms.isDeleted) ?? throw new Exception("Mission Skill not found.");

            bool skillAlreadyExists = conn.MissionSkills
                .Any(ms => ms.Id != model.ID
                    && ms.SkillName.ToLower() == model.SkillName.ToLower()
                    && !ms.isDeleted);

            if (skillAlreadyExists) throw new Exception("Skill Name Already Exist.");

            skillToUpdate.SkillName = model.SkillName;
            skillToUpdate.Status = model.Status;
            skillToUpdate.ModifiedDate = DateTime.UtcNow;

            conn.MissionSkills.Update(skillToUpdate);
            conn.SaveChanges();

            return "Updated Mission Skill Successfully..";
        }

        public string DeleteMissionSkill(int id)
        {
            conn.MissionSkills
                .Where(ms => ms.Id == id)
                .ExecuteUpdate(ms => ms.SetProperty(property => property.isDeleted, true));

            return "Deleted Mission Skill Successfully..";
        }
    }
}
