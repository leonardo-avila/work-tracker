using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using WorkTracker.Clock.Domain.Models;
using WorkTracker.Clock.Domain.Ports;
using WorkTracker.Domain.Core;

namespace WorkTracker.Clock.Domain.Services;

public class EmployeeService : IEmployeeService 
{
    private readonly IValidator<Employee> _employeeValidator;

    public EmployeeService(IValidator<Employee> employeeValidator)
    {
        _employeeValidator = employeeValidator;
    }

    public string GenerateEmployeeHash(string rm)
    {
        using SHA256 sha256Hash = SHA256.Create();
        // ComputeHash - returns byte array
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rm));
        // Convert byte array to a string
        StringBuilder builder = new();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }

    public void ValidateEmployee(Employee employee)
    {
        var validationResult = _employeeValidator.Validate(employee);

        if (!validationResult.IsValid)
        {
            throw new DomainException(validationResult.ToString());
        }
    }
}