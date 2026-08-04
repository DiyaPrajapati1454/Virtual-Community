using AngularAuthAPI.Dto;
using AngularAuthAPI.Models;
using AngularAuthAPI.Repositories;

namespace AngularAuthAPI.Services
{
    public class UserService
    {
        private readonly UserRepo _repo;
        public UserService(UserRepo repo)
        {
            _repo = repo;
        }
        public async Task AddUser(UserDetailReq user)
        {
            await this._repo.AddUser(user);
        }
        public User? GetUserById(int id) => this._repo.GetUserById(id);
        public async Task RegisterUser(RegisterUserDetails user)
        {
            await this._repo.Register(user);
        }
        public User?Login(string email,string password)
        {
            return this._repo.Login(email, password);
        }
        public UserDetail? GetUserDetails(int id)
        {
            return this._repo.GetUserDetails(id);
        }
        public async Task<bool> LoginUserProfileUpdate(UserDetailReq requestModel)
        {
            return await _repo.LoginUserProfileUpdate(requestModel);
        }

    }
}
