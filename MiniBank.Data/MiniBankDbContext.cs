using Microsoft.EntityFrameworkCore;
using MiniBank.Data.Accounts;
using MiniBank.Data.RemittanceHistories;
using MiniBank.Data.Users;

namespace MiniBank.Data
{
    public class MiniBankDbContext:DbContext
    {
        public MiniBankDbContext(DbContextOptions options) : base(options){}
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<AccountDbModel> Accounts { get; set; }
        public DbSet<RemittanceHistoryDbModel> RemittanceHistories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiniBankDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}