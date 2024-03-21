using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkTracker.Clock.Domain.Models;

namespace WorkTracker.Gateways.MySQL.Mappings
{
    [ExcludeFromCodeCoverage]
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(f => f.Hash)
                .HasColumnName("Hash")
                .IsRequired();

            builder.Property(f => f.Email)
                .HasColumnName("Email")
                .IsRequired();
        }
    }
}