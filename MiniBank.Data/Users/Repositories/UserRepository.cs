using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Domains.Users;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Core.Exception;

namespace MiniBank.Data.Users.Repositories
{
    public class UserRepository: IUserRepository
    {
        private static BlockingCollection<UserDbModel> Users = new BlockingCollection<UserDbModel>();

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
            dbUser.Id = Guid.NewGuid();
            Users.Add(dbUser);
        }

        public void Update(User user)
        {
            var dbUser = Users.FirstOrDefault(it => it.Id == user.Id);
            if (dbUser == null)
                throw new ValidationException("Пользователь с таким id не существует");
            dbUser.Email = user.Email;
            dbUser.Login = user.Login;
        }

        public void Delete(Guid id)
        {
            var dbUser = Users.FirstOrDefault(it => it.Id == id);
            if (dbUser == null)
                throw new ValidationException("Пользователя с таким id не существует");
            
            bool isExists=_accountRepository.AccountExists(id);
            if (isExists)
                throw new ValidationException("У пользователя есть привязанные банковские аккаунты");

            Users.TryTake(out dbUser);
        }

        public bool UserExists(Guid userId)
        {
            var isExists = Users.Any(it => it.Id == userId);
            if (isExists)
                return true;
            return false;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _mapper.Map<BlockingCollection<UserDbModel>, IEnumerable<User>>(Users);
        }
    }
}