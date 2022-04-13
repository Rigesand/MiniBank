using System.Threading.Tasks;
using MiniBank.Core;

namespace MiniBank.Data
{
    public class EfUnitOfWork:IUnitOfWork
    {
        private readonly MiniBankDbContext _context;

        public EfUnitOfWork(MiniBankDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}