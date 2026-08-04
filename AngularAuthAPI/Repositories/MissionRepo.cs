using AngularAuthAPI.Controllers;
using AngularAuthAPI.Data;
using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Repositories
{
    public class MissionRepo
    {
        private readonly Connection conn;
        public MissionRepo(Connection conn)
        {
            this.conn = conn;   
        }
        public async Task<List<Missions?>> GetMissionList()
        {
            return await conn.Missions.ToListAsync();
        }
        public async Task<MissionViewDto?> GetMissionById(int id)
        {
            return await conn.Missions.Where(m => m.Id == id).Select(m => new MissionViewDto()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).FirstOrDefaultAsync();
        }
        public async Task<string> AddMission(AddMissionReq model)
        {
            var isExist = conn.Missions.Where(x =>
                            x.MissionTitle == model.MissionTitle
                            && x.StartDate == model.StartDate
                            && x.EndDate == model.EndDate
                            && x.CityId == model.CityId
                            && !x.isDeleted
                        ).FirstOrDefault();

            if (isExist != null) throw new Exception("Mission already exist!");
       //     Console.WriteLine("Received StartDate: " + model.StartDate + " Kind: " + model.StartDate.Kind);
        //    Console.WriteLine("Received EndDate: " + model.EndDate + " Kind: " + model.EndDate.Kind);

            model.StartDate = DateTime.SpecifyKind(model.StartDate.Date, DateTimeKind.Utc);
            model.EndDate= DateTime.SpecifyKind(model.EndDate.Date, DateTimeKind.Utc);
          //  Console.WriteLine($"StartDate: {model.StartDate}, Kind: {model.StartDate.Kind}");
            //Console.WriteLine($"EndDate: {model.EndDate}, Kind: {model.EndDate.Kind}");
            Missions missions = new Missions()
            {
                MissionTitle = model.MissionTitle,
                MissionDescription = model.MissionDescription,
                MissionImages = model.MissionImages,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CountryId = model.CountryId,
                CityId = model.CityId,
                TotalSheets = model.TotalSheets,
                MissionThemeId = model.MissionThemeId,
                MissionSkillId = model.MissionSkillId,
                MissionOrganisationName = "",
                MissionOrganisationDetail = "",
                MissionType = "",
                MissionDocuments = "",
                MissionAvailability = "",
                MissionVideoUrl = "",
                // RegistrationDeadLine = model.RegistrationDeadLine,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                Country= (Country)conn.Country.Where(c => c.Id == model.CountryId),
                City=(City)conn.City.Where(c=>c.Id== model.CityId),
                MissionTheme=(MissionTheme)conn.MissionThemes.Where(c=>c.Id == model.MissionThemeId),
                MissionSkill=(MissionSkill)conn.MissionSkills.Where(c=>c.Id==model.MissionSkillId),

            };
            await conn.Missions.AddAsync(missions);
            conn.SaveChanges();

            return "Added!";
        }
        public async Task<IList<Missions>> ClientSideMissionList()
        {
            return await conn.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
               .Include(m => m.MissionTheme)
               .Include(m=>m.MissionSkill)
               .Include(m=>m.MissionApplications)
                .Where(m => !m.isDeleted)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }
        public async Task<bool> ApplyMission(AddMissionApplicationReq req)
        {
            try
            {
                var mission=conn.Missions.Where(x=>x.Id== req.MissionId).FirstOrDefault();
                if (req == null){
                    throw new Exception("Mission not found");
                }
                var application=conn.MissionApplications.Where(x=>x.MissionId==req.MissionId && x.UserId==req.UserId).FirstOrDefault();
                if (application != null) { throw new Exception("Already applied"); }
                MissionApplication app = new MissionApplication()
                {
                    UserId = req.UserId,
                    MissionId = req.MissionId,
                    AppliedDate=req.AppliedDate,
                    Seats=req.Sheet,
                    Status=req.Status,
                    isDeleted=false,
                    CreatedDate=DateTime.UtcNow
                };
                mission.TotalSheets -= req.Sheet;
                await conn.MissionApplications.AddAsync(app);
                conn.Missions.Update(mission);
                await conn.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public List<MissionApplication> GetMissionApplicationList()
        {
            return conn.MissionApplications.Where(x => !x.isDeleted).ToList();
        }
        public async Task<bool> MissionApplicationApprove(UpdateMissionApplicationReq missionApplication)
        {
            var tMissionApp =conn.MissionApplications.Where(x => x.Id == missionApplication.ID).FirstOrDefault();

            if (tMissionApp == null) throw new Exception("Mission application not found");

            tMissionApp.Status = true;
            tMissionApp.ModifiedDate = DateTime.UtcNow;

            conn.MissionApplications.Update(tMissionApp);
            await conn.SaveChangesAsync();
            return true;
        }


    }
}
