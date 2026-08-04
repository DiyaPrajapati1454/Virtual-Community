using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AngularAuthAPI.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        public int CountryId { get; set; }

        public string CityName { get; set; }

        public virtual ICollection<Missions> Missions { get; set; } = [];
    }
}
