using System.Collections.Generic;
using AutoMapper;
using MiniBank.Core.Domains.TranslationHistories;
using MiniBank.Core.Domains.TranslationHistories.Repositories;

namespace MiniBank.Data.TranslationHistories.Repositories
{
    public class TranslationRepository: ITranslationRepository
    {
        public static List<TranslationHistoryDbModel> TranslationHistories = new List<TranslationHistoryDbModel>();
        private readonly IMapper _mapper;

        public TranslationRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddTranslationHistory(TranslationHistory history)
        {
            var dbHistory = _mapper.Map<TranslationHistory, TranslationHistoryDbModel>(history);
            TranslationHistories.Add(dbHistory);
        }
    }
}
