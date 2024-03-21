using WorkTracker.Clock.UseCase.InputViewModels;

namespace WorkTracker.Clock.UseCase.Ports
{
	public interface IEmployeeUseCase
	{
		Task AddEmployee(EmployeeViewModel employeeViewModel);
	}
}