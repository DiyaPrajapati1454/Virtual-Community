using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
    public class UserSkill:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Skill { get; set; }

        public int UserId { get; set; }
    }
}
