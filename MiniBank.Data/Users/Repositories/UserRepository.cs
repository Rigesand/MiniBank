using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Domains.Users;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Core.Exception;

namespace MiniBank.Data.Users.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly IBankAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly MiniBankDbContext _context;
        public UserRepository(IMapper mapper, IBankAccountRepository accountRepository, MiniBankDbContext context)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _context = context;
        }
        
        public async Task Create(User newUser)
        {
            var dbUser = _mapper.Map<User, UserDbModel>(newUser);
            dbUser.Id = Guid.NewGuid();
            await _context.Users.AddAsync(dbUser);
        }

        public async Task Update(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(it => it.Id == user.Id);
            if (dbUser == null)
                throw new ValidationException("Пользователь с таким id не существует");
            dbUser.Email = user.Email;
            dbUser.Login = user.Login;
        }

        public async Task Delete(Guid id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(it => it.Id == id);
            if (dbUser == null)
                throw new ValidationException("Пользователя с таким id не существует");
            
            bool isExists=await _accountRepository.AccountExists(id);
            if (isExists)
                throw new ValidationException("У пользователя есть привязанные банковские аккаунты");
            
            _context.Users.Remove(dbUser);
        }

        public async Task<bool> UserExists(Guid userId)
        {
            var isExists = await _context.Users
                .AsNoTracking()
                .AnyAsync(it => it.Id == userId);
            if (isExists)
                return true;
            return false;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users=await _context.Users
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<List<UserDbModel>, IEnumerable<User>>(users);
        }

        public async Task<bool> IsExist(string login)
        {
            var isExists = await _context.Users
                .AsNoTracking()
                .AnyAsync(it => it.Login == login);
            return isExists;
        }
    }
}