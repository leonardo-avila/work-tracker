using WorkTracker.Clock.UseCase.InputViewModels;

namespace WorkTracker.Clock.UseCase.Ports
{
	public interface IEmployeeUseCases
	{
		Task AddEmployee(EmployeeViewModel employeeViewModel);
	}
}