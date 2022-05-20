using System;
using System.Threading.Tasks;
using MiniBank.Core.Domains.RemittanceHistories.Repositories;

namespace MiniBank.Core.Domains.RemittanceHistories.Services
{
    public class RemittanceHistoryService: IRemittanceHistoryService
    {
        private readonly IRemittanceRepository _remittanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemittanceHistoryService(IRemittanceRepository remittanceRepository, IUnitOfWork unitOfWork)
        {
            _remittanceRepository = remittanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddRemittanceHistory(RemittanceHistory history)
        {
            await _remittanceRepository.AddRemittanceHistory(history);
            await _unitOfWork.SaveChanges();
        }
    }
}