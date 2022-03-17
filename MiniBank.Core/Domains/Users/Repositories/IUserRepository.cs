using System.Collections.Generic;

namespace MiniBank.Core.Domains.Users.Repositories
{
    public interface IUserRepository
    {
        void Create(User newUser);
        void Update(User user);
        void Delete(string id);
        bool UserExists(string userId);
        List<User> GetAllUsers();
    }
}