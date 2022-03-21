namespace MiniBank.Core.Domains.RemittanceHistories.Repositories
{
    public interface IRemittanceRepository
    {
        void AddRemittanceHistory(RemittanceHistory history);
    }
}