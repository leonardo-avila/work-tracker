using Microsoft.EntityFrameworkCore;
using WorkTracker.Clock.Domain.Models;
using WorkTracker.Clock.Domain.Repositories;
using WorkTracker.Gateways.MySQL.Contexts;

namespace WorkTracker.Gateways.MySQL.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        protected readonly ClockContext Db;
        protected readonly DbSet<Employee> DbSet;

        public EmployeeRepository(ClockContext context) : base (context)
        {
            Db = context;
            DbSet = Db.Set<Employee>();
        }

        public async Task<Employee> GetEmployee(string employeeHash)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Hash == employeeHash);
        }
    }
}