using WorkTracker.Clock.Domain.Models;
using WorkTracker.Clock.Domain.Repositories;
using WorkTracker.Gateways.MySQL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace WorkTracker.Gateways.MySQL.Repositories
{
	public class PunchesRepository : RepositoryBase<Punch>, IPunchesRepository
	{
        protected readonly ClockContext Db;
        protected readonly DbSet<Punch> DbSet;

        public PunchesRepository(ClockContext context) : base (context)
        {
            Db = context;
            DbSet = Db.Set<Punch>();
        }

        public async Task<IEnumerable<Punch>> GetPunches(string employeeHash)
        {
            var currentDate = DateTime.UtcNow.Date;
            return await DbSet.Where(x => x.EmployeeHash == employeeHash 
                && x.Timestamp.Date == currentDate).ToListAsync();
        }

        public async Task<IEnumerable<Punch>> GetMonthlyPunches(string employeeHash)
        {
            var currentDate = DateTime.UtcNow.Date;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return await DbSet.Where(x => x.EmployeeHash == employeeHash 
                && x.Timestamp.Date >= firstDayOfMonth 
                && x.Timestamp.Date <= lastDayOfMonth).ToListAsync();
        }
    }
}