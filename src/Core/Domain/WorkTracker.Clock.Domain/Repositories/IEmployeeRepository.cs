using WorkTracker.Clock.Domain.Models;
using WorkTracker.Data.Core;

namespace WorkTracker.Clock.Domain.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetEmployee(string rm);
}