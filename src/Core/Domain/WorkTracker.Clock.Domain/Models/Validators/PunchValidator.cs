using FluentValidation;

namespace WorkTracker.Clock.Domain.Models.Validators
{
	public class PunchValidator : AbstractValidator<Punch>
	{
		public PunchValidator()
		{
			RuleFor(p => p.EmployeeHash).NotNull();
			RuleFor(p => p.Timestamp).NotNull();
			RuleFor(p => p.IsApproved).NotNull();
			RuleFor(p => p.Type).IsInEnum();
		}
	}
}