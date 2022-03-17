namespace MiniBank.Core.Domains.TranslationHistories.Repositories
{
    public interface ITranslationRepository
    {
        void AddTranslationHistory(TranslationHistory history);
    }
}