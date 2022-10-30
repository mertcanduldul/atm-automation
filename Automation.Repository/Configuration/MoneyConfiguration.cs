using Automation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Automation.Repository.Configuration;

public class MoneyConfiguration : IEntityTypeConfiguration<Money>
{
    public void Configure(EntityTypeBuilder<Money> builder)
    {
        builder.ToTable("Money");
        builder.HasKey(x => x.ID_MONEY);
        builder.Property(x => x.ID_MONEY).UseIdentityColumn();
        builder.Property(x => x.MONEY_NAME).IsRequired().HasColumnType("nvarchar(50)");
        builder.Property(x => x.MONEY_VALUE).IsRequired().HasColumnType("int");
        builder.Property(x => x.MONEY_TYPE_ID).IsRequired().HasColumnType("int");
    }
}