using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBank.Core.Domains.Users.Repositories
{
    public interface IUserRepository
    {
        Task Create(User newUser);
        Task Update(User user);
        Task Delete(Guid id);
        Task<bool> UserExists(Guid userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> IsExist(string login);
    }
}