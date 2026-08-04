using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Dto
{
    public class MissionThemeDto
    {
        [Key]
        public int Id {  get; set; }
        public string ThemeName { get; set; }
        public string Status { get; set; }
    }
}
