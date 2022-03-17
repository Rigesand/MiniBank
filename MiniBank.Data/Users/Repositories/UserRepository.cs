using System.Collections.Generic;
using AutoMapper;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Domains.Users;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Core.Exception;

namespace MiniBank.Data.Users.Repositories
{
    public class UserRepository: IUserRepository
    {
        public static List<UserDbModel> Users = new List<UserDbModel>();

        private readonly IBankAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public UserRepository(IMapper mapper, IBankAccountRepository accountRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }
        
        public void Create(User newUser)
        {
            var dbUser = _mapper.Map<User, UserDbModel>(newUser);
            Users.Add(dbUser);
        }

        public void Update(User user)
        {
            var dbUser = Users.Find(it => it.Id == user.Id);
            if (dbUser is null)
                throw new ValidationException("Пользователь с таким id не существует");
            dbUser.Email = user.Email;
            dbUser.Login = user.Login;
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ValidationException("id не может быть пустым или null");
            
            var index = Users.FindIndex(it => it.Id == id);
            if (index == -1)
                throw new ValidationException("Пользователя с таким id не существует");
            
            bool isExists=_accountRepository.AccountExists(id);
            if (isExists)
                throw new ValidationException("У пользователя есть привязанные банковские аккаунты");
            
            Users.RemoveAt(index);
        }

        public bool UserExists(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ValidationException("Id не может быть пустым или null");
            var isExists = Users.Find(it => it.Id == userId);
            if (isExists is not null)
                return true;
            return false;
        }

        public List<User> GetAllUsers()
        {
            return _mapper.Map<List<UserDbModel>, List<User>>(Users);
        }
    }
}