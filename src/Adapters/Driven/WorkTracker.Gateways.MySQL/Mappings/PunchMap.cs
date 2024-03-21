using System.Diagnostics.CodeAnalysis;
using WorkTracker.Clock.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkTracker.Gateways.MySQL.Mappings
{
    [ExcludeFromCodeCoverage]
    public class PunchMap : IEntityTypeConfiguration<Punch>
    {
        public void Configure(EntityTypeBuilder<Punch> builder)
        {
            builder.ToTable("Punch");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(f => f.EmployeeHash)
                .HasColumnName("EmployeeHash")
                .IsRequired();

            builder.Property(f => f.Timestamp)
                .HasColumnName("Timestamp")
                .IsRequired();

            builder.Property(f => f.UpdatedTimestamp)
                .HasColumnName("UpdatedTimestamp");

            builder.Property(f => f.IsApproved)
                .HasColumnName("IsApproved")
                .IsRequired();

            builder.Property(f => f.Type)
                .HasColumnName("Type")
                .HasConversion<string>()
                .IsRequired();
        }
    }
}