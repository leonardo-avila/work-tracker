using System.Diagnostics.CodeAnalysis;
using WorkTracker.Clock.Domain.Models;
using WorkTracker.Gateways.MySQL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace WorkTracker.Gateways.MySQL.Contexts
{
    [ExcludeFromCodeCoverage]
    public class ClockContext : DbContext
    {
        public ClockContext(DbContextOptions<ClockContext> options) : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Punch> Punches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Clock");

            modelBuilder.ApplyConfiguration(new PunchMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}