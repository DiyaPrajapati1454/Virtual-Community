using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string phone_No {  get; set; }  

        public string Email { get; set; }
        public string type {  get; set; }   
        public string Password { get; set; }


    }
}
