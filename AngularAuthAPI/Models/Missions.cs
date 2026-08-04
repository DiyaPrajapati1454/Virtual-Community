using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularAuthAPI.Models
{
    public class Missions : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string MissionTitle { get; set; }

        public string MissionDescription { get; set; }

        public string MissionOrganisationName { get; set; }

        public string MissionOrganisationDetail { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string MissionType { get; set; }

        public int? TotalSheets { get; set; }

        public DateTime? RegistrationDeadLine { get; set; }

        public int MissionThemeId { get; set; }

        public int MissionSkillId { get; set; }

        public string MissionImages { get; set; }

        public string MissionDocuments { get; set; }

        public string MissionAvailability { get; set; }

        public string MissionVideoUrl { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; } = null!;

        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; } = null!;

        [ForeignKey(nameof(MissionThemeId))]
        public virtual MissionTheme MissionTheme { get; set; } = null!;
        [ForeignKey(nameof(MissionSkillId))]
        public virtual MissionSkill MissionSkill { get; set; } = null!;
        public virtual ICollection<MissionApplication> MissionApplications { get; set; } = [];

    }
}
