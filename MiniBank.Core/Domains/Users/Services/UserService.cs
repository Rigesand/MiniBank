using System;
using System.Collections.Generic;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Core.Exception;

namespace MiniBank.Core.Domains.Users.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Create(User newUser)
        {
            if (newUser is null)
                throw new ValidationException("Пользователь не может быть равен null");
            
            if (string.IsNullOrEmpty(newUser.Login) || newUser.Login.Length > 20)
            {
                throw new ValidationException("Логин не может быть пустым,равен null или его длина более 20 символов");
            }

            if (string.IsNullOrEmpty(newUser.Email))
                throw new ValidationException("Email не может быть пустым или null");
            
            newUser.Id = Guid.NewGuid().ToString();
            
            _userRepository.Create(newUser);
        }

        public void Update(User user)
        {
            if (user is null)
                throw new ValidationException("Пользователь не может быть равен null");
            
            if (string.IsNullOrEmpty(user.Login) || user.Login.Length > 20)
            {
                throw new ValidationException("Логин не может быть пустым,равен null или его длина более 20 символов");
            }
            
            if (string.IsNullOrEmpty(user.Email))
                throw new ValidationException("Email не может быть пустым или null");
            
            _userRepository.Update(user);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ValidationException("Id не может быть пустым или null");
            _userRepository.Delete(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}