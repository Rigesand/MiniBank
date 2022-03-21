namespace MiniBank.Core.Domains.RemittanceHistories.Services
{
    public interface IRemittanceHistoryService
    {
        void AddRemittanceHistory(RemittanceHistory history);
    }
}