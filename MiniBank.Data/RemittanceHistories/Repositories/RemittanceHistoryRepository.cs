using System;
using System.Threading.Tasks;
using AutoMapper;
using MiniBank.Core.Domains.RemittanceHistories;
using MiniBank.Core.Domains.RemittanceHistories.Repositories;

namespace MiniBank.Data.RemittanceHistories.Repositories
{
    public class RemittanceHistoryRepository: IRemittanceRepository
    {
        private readonly IMapper _mapper;
        private readonly MiniBankDbContext _context;

        public RemittanceHistoryRepository(IMapper mapper, MiniBankDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task AddRemittanceHistory(RemittanceHistory history)
        {
            var dbHistory = _mapper.Map<RemittanceHistory, RemittanceHistoryDbModel>(history);
            dbHistory.Id = Guid.NewGuid();
            await _context.RemittanceHistories.AddAsync(dbHistory);
        }
    }
}