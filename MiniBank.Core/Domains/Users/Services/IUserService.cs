using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBank.Core.Domains.Users.Services
{
    public interface IUserService
    {
        Task Create(User newUser);
        Task Update(User user);
        Task Delete(Guid id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> IsExist(string login);
    }
}