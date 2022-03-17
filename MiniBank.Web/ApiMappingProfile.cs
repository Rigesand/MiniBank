using AutoMapper;
using MiniBank.Core.Domains.BankAccounts;
using MiniBank.Core.Domains.Users;
using MiniBank.Web.Controllers.Accounts.Dto;
using MiniBank.Web.Controllers.Users.Dto;

namespace MiniBank.Web
{
    public class ApiMappingProfile:Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<GetUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<GetAccountDto, Account>().ReverseMap();
            CreateMap<CreateAccountDto, Account>().ReverseMap();
        }
    }
}