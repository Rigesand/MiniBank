using System;
using MiniBank.Core.Domains.TranslationHistories.Repositories;

namespace MiniBank.Core.Domains.TranslationHistories.Services
{
    public class TranslationService: ITranslationService
    {
        private readonly ITranslationRepository _translationRepository;

        public TranslationService(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public void AddTranslationHistory(TranslationHistory history)
        {
            history.Id = Guid.NewGuid().ToString();
            _translationRepository.AddTranslationHistory(history);
        }
    }
}