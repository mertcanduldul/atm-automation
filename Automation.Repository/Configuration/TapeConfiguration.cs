using Automation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Automation.Repository.Configuration;

public class TapeConfiguration : IEntityTypeConfiguration<Tape>
{
    public void Configure(EntityTypeBuilder<Tape> builder)
    {
        builder.ToTable("Tape");
        builder.HasKey(x => x.ID_TAPE);
        builder.Property(x => x.ID_TAPE).UseIdentityColumn();
        builder.Property(x => x.TAPE_MONEY_TYPE_ID).IsRequired().HasColumnType("int");
        builder.Property(x => x.TAPE_STATE_TYPE_ID).IsRequired().HasColumnType("int");
    }
}