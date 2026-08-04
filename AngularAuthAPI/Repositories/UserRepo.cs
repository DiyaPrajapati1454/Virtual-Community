using System.ComponentModel.DataAnnotations.Schema;
using AngularAuthAPI.Data;
using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AngularAuthAPI.Repositories
{
    public class UserRepo
    {
        private readonly Connection conn;
        public UserRepo(Connection connection)
        {
            this.conn = connection;
        }
        public async Task AddUser(UserDetailReq user)
        {
            UserDetail user1 = new UserDetail()
            {
                UserId = user.UserId,
                Name = user.Name,
                Surname = user.Surname,
                EmployeeId = user.EmployeeId,
                Manager = user.Manager,
                Title = user.Title,
                Department = user.Department,
                MyProfile = user.MyProfile,
                WhyIVolunteer = user.WhyIVolunteer,
                CountryId=user.CountryId,
                CityId=user.CityId,
                Availability=user.Avilability,
                LinkedInUrl=user.LinkdInUrl,
                MySkills=user.MySkills,
                UserImage=user.UserImage,
                Status=user.Status,
                User= conn.Users.Where(x => x.Id == user.UserId && !x.isDeleted).FirstOrDefault(),
                CreatedDate=DateTime.UtcNow,
            };
            await conn.UserDetails.AddAsync(user1);
            await conn.SaveChangesAsync();
        }
        public User? GetUserById(int id)
        {
            var user=conn.Users.Where(x=>x.Id==id && !x.isDeleted).FirstOrDefault();
            return user;
        }
        public User? Login(string email, string password) {
            var user = conn.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            return user;
        }
        public UserDetail GetUserDetails(int id)
        {
            var user = conn.UserDetails.Where(x => x.Id == id && !x.isDeleted).FirstOrDefault();
            return user;
        }
        public async Task<string> Register(RegisterUserDetails model)
        {
            var isExist = conn.Users.Where(x => x.Email == model.Email && !x.isDeleted).FirstOrDefault();

            if (isExist != null) throw new Exception("Email already exist");

            User user = new User()
            {
                First_Name = model.First_Name,
                Last_Name = model.Last_Name,
                Email = model.Email,
                Password = model.Password,
                phone_No = model.phone_No,
                type = "User",
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
            };

            await conn.Users.AddAsync(user);
            conn.SaveChanges();
            return "User Added!";
        }
        public async Task<bool> LoginUserProfileUpdate(UserDetailReq requestModel)
        {
            try
            {
                var user = conn.Users.Where(x => x.Id == requestModel.UserId).FirstOrDefault();

                if (user == null) throw new Exception("Not Found!");

                var userDetails = conn.UserDetails.Where(x => x.UserId == requestModel.UserId).FirstOrDefault();

                if (userDetails == null)
                {
                    // Add User Details
                    UserDetail userDetail = new UserDetail()
                    {
                        UserId = requestModel.UserId,
                        Availability = requestModel.Avilability,
                        CityId = requestModel.CityId,
                        CountryId = requestModel.CountryId,
                        Department = requestModel.Department,
                        EmployeeId = requestModel.EmployeeId,
                        LinkedInUrl = requestModel.LinkdInUrl,
                        Manager = requestModel.Manager,
                        MyProfile = requestModel.MyProfile,
                        MySkills = requestModel.MySkills,
                        Surname = requestModel.Surname,
                        Name = requestModel.Name,
                        UserImage = requestModel.UserImage,
                        WhyIVolunteer = requestModel.WhyIVolunteer,
                        Status = requestModel.Status,
                        Title = requestModel.Title,

                        isDeleted = false,
                        CreatedDate = DateTime.UtcNow,
                    };

                    await conn.UserDetails.AddAsync(userDetail);
                }
                else
                {
                    // Update User Details
                    userDetails.UserId = requestModel.UserId;
                    userDetails.Availability = requestModel.Avilability;
                    userDetails.CityId = requestModel.CityId;
                    userDetails.CountryId = requestModel.CountryId;
                    userDetails.Department = requestModel.Department;
                    userDetails.EmployeeId = requestModel.EmployeeId;
                    userDetails.LinkedInUrl = requestModel.LinkdInUrl;
                    userDetails.Manager = requestModel.Manager;
                    userDetails.MyProfile = requestModel.MyProfile;
                    userDetails.MySkills = requestModel.MySkills;
                    userDetails.Surname = requestModel.Surname;
                    userDetails.Name = requestModel.Name;
                    userDetails.UserImage = requestModel.UserImage;
                    userDetails.WhyIVolunteer = requestModel.WhyIVolunteer;
                    userDetails.Status = requestModel.Status;
                    userDetails.Title = requestModel.Title;

                    userDetails.ModifiedDate = DateTime.UtcNow;

                    conn.UserDetails.Update(userDetails);
                }

                user.First_Name = requestModel.Name;
                user.Last_Name = requestModel.Surname;

                conn.Users.Update(user);
                await conn.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
