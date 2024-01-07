using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationContext.Configurations;

/// <summary>
/// Настройка таблицы пользователей.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<Participant>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Fio);
    }
}