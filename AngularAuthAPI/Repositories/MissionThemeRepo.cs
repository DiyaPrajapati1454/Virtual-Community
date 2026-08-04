using AngularAuthAPI.Data;
using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Repositories
{
    public class MissionThemeRepo
    {
        private readonly Connection conn;
        public MissionThemeRepo(Connection conn)
        {
            this.conn = conn;
        }
        public async Task<bool> AddMissionTheme(AddMissionThemeDto theme_dto)
        {
            MissionTheme theme=new MissionTheme()
            {
                ThemeName = theme_dto.ThemeName,
                Status = theme_dto.Status,
                CreatedDate=DateTime.UtcNow,
                isDeleted=false
            };
            conn.MissionThemes.Add(theme);
            await conn.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteMissionTheme(int id)
        {
            var theme=await conn.MissionThemes.FindAsync(id);
            if(theme != null)
            {
                theme.ModifiedDate = DateTime.UtcNow;
                theme.isDeleted = true;
                try
                {
                    // conn.MissionThemes.Update(theme);
                 //   conn.Entry(theme).Property(x => x.isDeleted).IsModified = true;
                  //  conn.Entry(theme).Property(x => x.ModifiedDate).IsModified = true;
                    await conn.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public Task<List<MissionThemeDto>> GetAllMissionTheme()
        {
            return conn.MissionThemes
                 .Where(t => t.isDeleted == false)
                .Select(m => new MissionThemeDto()
                {
                    Id = m.Id,
                    Status = m.Status,
                    ThemeName = m.ThemeName
                })
                .ToListAsync();
        }

        public Task<MissionThemeDto?> GetMissionThemeById(int missionThemeId)
        {
            return conn.MissionThemes
                .Where(m => m.Id == missionThemeId)
                .Select(m => new MissionThemeDto()
                {
                    Id = m.Id,
                    Status = m.Status,
                    ThemeName = m.ThemeName,
                   
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateMissionTheme(MissionThemeDto missionTheme)
        {
            var missionThemeExistingInDb = await conn.MissionThemes.FindAsync(missionTheme.Id);

            if (missionThemeExistingInDb == null)
                return false;

            missionThemeExistingInDb.ThemeName = missionTheme.ThemeName;
            missionThemeExistingInDb.Status = missionTheme.Status;
            missionThemeExistingInDb.ModifiedDate = DateTime.UtcNow;
           // conn.MissionThemes.Update(missionTheme);
            await conn.SaveChangesAsync();

            return true;
        }

    }
}
