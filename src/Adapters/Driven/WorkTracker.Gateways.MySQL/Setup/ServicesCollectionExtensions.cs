using WorkTracker.Gateways.MySQL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServicesCollectionExtensions
	{
		public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var connectionString = configuration.GetConnectionString("DefaultConnection")!;

            services.AddDbContext<ClockContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), o => {
                    o.SchemaBehavior(MySqlSchemaBehavior.Translate,
                        (schema, entity) => $"{schema ?? "dbo"}_{entity}");
                }));
        }
    }

    public class ClockContextFactory : IDesignTimeDbContextFactory<ClockContext>
    {
        private readonly string _connectionString = "Server=localhost;Port=3306;Database=worktracker;Uid=user;Pwd=uSeRpAsSwOrD;";

        public ClockContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClockContext>();
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString), o => {
                o.SchemaBehavior(MySqlSchemaBehavior.Translate,
                    (schema, entity) => $"{schema ?? "dbo"}_{entity}");
            });

            return new ClockContext(optionsBuilder.Options);
        }
    }
}