using WorkTracker.Clock.Domain.Models;

namespace WorkTracker.Clock.Domain.Ports
{
    public interface IEmployeeService
    {
        string GenerateEmployeeHash(string rm);
        void ValidateEmployee(Employee employee);
    }
}