using System.Threading.Tasks;

namespace MiniBank.Core.Domains.RemittanceHistories.Repositories
{
    public interface IRemittanceRepository
    {
        Task AddRemittanceHistory(RemittanceHistory history);
    }
}