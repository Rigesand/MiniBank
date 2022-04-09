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
            await _userRepository.Create(newUser);
            await _unitOfWork.SaveChanges();
        }

        public async Task Update(User user)
        {
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
            return await _userRepository.IsExist(login);
        }
    }
}