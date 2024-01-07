using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationContext.Configurations;

/// <summary>
/// Настройки таблицы договоров.
/// </summary>
public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Name);

        builder.HasOne(e => e.Participant)
            .WithMany()
            .HasForeignKey("participant_id");
    }
}