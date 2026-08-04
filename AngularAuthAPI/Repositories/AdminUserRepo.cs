using AngularAuthAPI.Data;
using AngularAuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Repositories
{
    public class AdminUserRepo
    {
        private readonly Connection conn;
        public AdminUserRepo(Connection conn)
        {
            this.conn = conn;
        }
        public List<User> UserDetailsList()
        {
            var res = conn.Users.Where(x => x.type == "User").Select(x => new User
            {
                Id=x.Id,
                First_Name=x.First_Name,
                Last_Name=x.Last_Name,
                Email=x.Email,
                phone_No=x.phone_No,
                CreatedDate=x.CreatedDate,
                ModifiedDate=x.ModifiedDate,
                isDeleted=x.isDeleted
            });
            return res.ToList();
        }
        public string DeleteUser(int id)
        {
            var user = conn.Users.Where(x => x.Id == id).FirstOrDefault();

            if (user == null) return("Account does't exist!");

            user.isDeleted = true;

            //user.EmailAddress = model.EmailAddress

            user.ModifiedDate = DateTime.UtcNow;
            try
            {
                conn.Users.Update(user);
                conn.SaveChanges();
                return "Account deleted!";
            }catch(DbUpdateException dbEx)
            {
                var detail = dbEx.InnerException?.Message ?? dbEx.Message;
                return $"Failed to delete user. Database error: {detail}";
            }catch(Exception ex)
            {
                return $"Failed to delete user. Unexpected error: {ex.Message}";
            }
        }
    }
}
