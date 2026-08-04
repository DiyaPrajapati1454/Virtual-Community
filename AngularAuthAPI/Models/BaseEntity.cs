namespace AngularAuthAPI.Models
{
    public class BaseEntity
    {
        public DateTime? CreatedDate { get; set; } 
        public DateTime? ModifiedDate { get; set; }
        public bool isDeleted { get; set; } 
    }
}
