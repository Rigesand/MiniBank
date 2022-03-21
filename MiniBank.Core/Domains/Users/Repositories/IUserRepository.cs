using System;
using System.Collections.Generic;

namespace MiniBank.Core.Domains.Users.Repositories
{
    public interface IUserRepository
    {
        void Create(User newUser);
        void Update(User user);
        void Delete(Guid id);
        bool UserExists(Guid userId);
        IEnumerable<User> GetAllUsers();
    }
}