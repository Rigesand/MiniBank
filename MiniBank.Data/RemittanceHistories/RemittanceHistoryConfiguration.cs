using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiniBank.Data.RemittanceHistories
{
    public class RemittanceHistoryConfiguration:IEntityTypeConfiguration<RemittanceHistoryDbModel>
    {
        public void Configure(EntityTypeBuilder<RemittanceHistoryDbModel> builder)
        {
            builder.ToTable("remittanceHistory");

            builder.HasKey(it => it.Id);
        }
    }
}