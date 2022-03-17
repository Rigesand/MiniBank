using System.Collections.Generic;

namespace MiniBank.Core.Domains.Users.Services
{
    public interface IUserService
    {
        void Create(User newUser);
        void Update(User user);
        void Delete(string id);
        List<User> GetAllUsers();
    }
}