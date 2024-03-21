using FluentValidation;

namespace WorkTracker.Clock.Domain.Models.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Hash)
                .NotEmpty();
        }
    }
}