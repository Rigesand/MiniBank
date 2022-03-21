using System;
using MiniBank.Core.Domains.RemittanceHistories.Repositories;

namespace MiniBank.Core.Domains.RemittanceHistories.Services
{
    public class RemittanceHistoryService: IRemittanceHistoryService
    {
        private readonly IRemittanceRepository _remittanceRepository;

        public RemittanceHistoryService(IRemittanceRepository remittanceRepository)
        {
            _remittanceRepository = remittanceRepository;
        }

        public void AddRemittanceHistory(RemittanceHistory history)
        {
            _remittanceRepository.AddRemittanceHistory(history);
        }
    }
}