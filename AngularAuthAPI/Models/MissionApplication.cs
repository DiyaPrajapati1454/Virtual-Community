using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularAuthAPI.Models
{
    public class MissionApplication : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int MissionId { get; set; }

        public int UserId { get; set; }

        public DateTime AppliedDate { get; set; }

        public bool Status { get; set; }

        public int Seats { get; set; }

        [ForeignKey(nameof(MissionId))]
        public virtual Missions Mission { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;
    }
}
