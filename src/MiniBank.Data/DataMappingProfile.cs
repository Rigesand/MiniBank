using AutoMapper;
using MiniBank.Core.Domains.BankAccounts;
using MiniBank.Core.Domains.RemittanceHistories;
using MiniBank.Core.Domains.Users;
using MiniBank.Data.Accounts;
using MiniBank.Data.RemittanceHistories;
using MiniBank.Data.Users;

namespace MiniBank.Data
{
    public class DataMappingProfile: Profile
    {
        public DataMappingProfile()
        {
            CreateMap<User, UserDbModel>().ReverseMap();
            CreateMap<Account, AccountDbModel>().ReverseMap();
            CreateMap<RemittanceHistory, RemittanceHistoryDbModel>().ReverseMap();
        }
    }
}