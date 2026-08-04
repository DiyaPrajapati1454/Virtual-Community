using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
    public class MissionTheme: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string ThemeName { get; set; }
        public string Status { get; set; }
    }
}
