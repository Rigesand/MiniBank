using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniBank.Core.Domains.Users.Repositories;

namespace MiniBank.Core.Domains.Users.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(User newUser)
        {
            if (newUser is null)
            {
                throw new ArgumentNullException("Пользователь не может быть равен null");
            }
            if (newUser.Email==null||newUser.Login == null)
            {
                throw new ArgumentNullException("Email или Login не может быть равен null");
            }
            await _userRepository.Create(newUser);
            await _unitOfWork.SaveChanges();
        }

        public async Task Update(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException("Пользователь не может быть равен null");
            }
            if (user.Email==null||user.Login == null||user.Id==null)
            {
                throw new ArgumentNullException("Id,Email или Login не может быть равен null");
            }
            await _userRepository.Update(user);
            await _unitOfWork.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            await _userRepository.Delete(id);
            await _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<bool> IsExist(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException("Логин не может быть пустым или равен null");
            }
            return await _userRepository.IsExist(login);
        }
    }
}