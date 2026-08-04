using AngularAuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Data
{
    public class Connection : DbContext
    {
        public Connection(DbContextOptions<Connection> options): base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<MissionTheme> MissionThemes { get; set; }
        public DbSet<MissionSkill> MissionSkills { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Missions> Missions { get; set; }
        public DbSet<MissionApplication> MissionApplications { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
    }
}
