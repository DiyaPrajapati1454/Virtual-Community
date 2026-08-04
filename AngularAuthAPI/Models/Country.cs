using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AngularAuthAPI.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string CountryName { get; set; }

        public virtual ICollection<Missions> Missions { get; set; } = [];
    }
}
