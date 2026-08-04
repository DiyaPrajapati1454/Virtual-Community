using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
    public class MissionSkill :BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string SkillName { get; set; }

        public string Status { get; set; }
    }
}
