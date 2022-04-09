using System.Threading.Tasks;

namespace MiniBank.Core.Domains.RemittanceHistories.Services
{
    public interface IRemittanceHistoryService
    {
        Task AddRemittanceHistory(RemittanceHistory history);
    }
}