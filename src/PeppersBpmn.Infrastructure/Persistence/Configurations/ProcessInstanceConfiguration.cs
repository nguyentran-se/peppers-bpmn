using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeppersBpmn.Domain.Entities;
using PeppersBpmn.Domain.ValueObjects;

namespace PeppersBpmn.Infrastructure.Persistence.Configurations;

public class ProcessInstanceConfiguration : IEntityTypeConfiguration<ProcessInstance>
{
    public void Configure(EntityTypeBuilder<ProcessInstance> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CamundaInstanceId).IsRequired().HasMaxLength(255);
        builder.HasIndex(x => x.CamundaInstanceId).IsUnique();
        builder.Property(x => x.BusinessKey).HasMaxLength(255);
        builder.Property(x => x.StartedAt).IsRequired();
        builder.Property(x => x.EndedAt);

        builder.OwnsOne(x => x.ProcessKey, k =>
        {
            k.Property(p => p.Value).HasColumnName("ProcessKey").IsRequired().HasMaxLength(255);
            k.HasIndex(p => p.Value);
        });

        builder.Property(x => x.Status)
            .HasConversion(
                v => v.Value.ToString(),
                v => ProcessStatus.From(v.ToLower()))
            .HasMaxLength(50);
    }
}
