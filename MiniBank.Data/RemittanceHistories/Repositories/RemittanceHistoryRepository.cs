using System;
using System.Collections.Concurrent;
using AutoMapper;
using MiniBank.Core.Domains.RemittanceHistories;
using MiniBank.Core.Domains.RemittanceHistories.Repositories;

namespace MiniBank.Data.RemittanceHistories.Repositories
{
    public class RemittanceHistoryRepository: IRemittanceRepository
    {
        private static BlockingCollection<RemittanceHistoryDbModel> RemittanceHistories = new BlockingCollection<RemittanceHistoryDbModel>();
        private readonly IMapper _mapper;

        public RemittanceHistoryRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddRemittanceHistory(RemittanceHistory history)
        {
            var dbHistory = _mapper.Map<RemittanceHistory, RemittanceHistoryDbModel>(history);
            dbHistory.Id = Guid.NewGuid();
            RemittanceHistories.Add(dbHistory);
        }
    }
}
