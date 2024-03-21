using WorkTracker.Clock.Domain.Models;
using WorkTracker.Clock.Domain.Ports;
using WorkTracker.Clock.Domain.Repositories;
using WorkTracker.Clock.UseCase.InputViewModels;
using WorkTracker.Clock.UseCase.Ports;

namespace WorkTracker.Clock.UseCase.UseCases
{
    public class EmployeeUseCase : IEmployeeUseCase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeUseCase(IEmployeeService employeeService, IEmployeeRepository employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }
        public async Task AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var employeeHash = _employeeService.GenerateEmployeeHash(employeeViewModel.RM);
            var employee = new Employee(employeeViewModel.Email, employeeHash);
            _employeeService.ValidateEmployee(employee);

            var success = await _employeeRepository.Create(employee);
            if (!success)
            {
                throw new Exception("Error creating employee");
            }
        }
    }
}