using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeppersBpmn.Domain.Entities;

namespace PeppersBpmn.Infrastructure.Persistence.Configurations;

public class ProcessDefinitionConfiguration : IEntityTypeConfiguration<ProcessDefinition>
{
    public void Configure(EntityTypeBuilder<ProcessDefinition> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CamundaDeploymentId).IsRequired().HasMaxLength(255);
        builder.Property(x => x.CamundaDefinitionId).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.Version).IsRequired();
        builder.Property(x => x.ResourceName).HasMaxLength(500);
        builder.Property(x => x.DeployedAt).IsRequired();

        builder.OwnsOne(x => x.Key, k =>
        {
            k.Property(p => p.Value).HasColumnName("Key").IsRequired().HasMaxLength(255);
            k.HasIndex(p => p.Value);
        });
    }
}
