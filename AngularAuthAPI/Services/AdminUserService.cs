using AngularAuthAPI.Models;
using AngularAuthAPI.Repositories;

namespace AngularAuthAPI.Services
{
    public class AdminUserService
    {
        private readonly AdminUserRepo _repo;
        public AdminUserService(AdminUserRepo repo)
        {
            _repo = repo;
        }
        public List<User> UserDetailsList()
        {
            return _repo.UserDetailsList();
        }
        public string UserDelete(int id)
        {
            return _repo.DeleteUser(id);
        }
    }
}
