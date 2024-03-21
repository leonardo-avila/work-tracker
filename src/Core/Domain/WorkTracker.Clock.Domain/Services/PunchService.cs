using WorkTracker.Clock.Domain.Models;
using WorkTracker.Clock.Domain.Models.Enums;
using WorkTracker.Clock.Domain.Ports;
using WorkTracker.Domain.Core;
using FluentValidation;

namespace WorkTracker.Clock.Domain.Services
{
    public class PunchService : IPunchService
	{
        private readonly IValidator<Punch> _punchValidator;

        public PunchService(IValidator<Punch> punchValidator)
        {
            _punchValidator = punchValidator;
        }

        public bool IsValidType(string type)
        {
            return Enum.IsDefined(typeof(PunchType), type);
        }

        public void ValidatePunch(Punch punch)
        {
            var validationResult = _punchValidator.Validate(punch);

            if (!validationResult.IsValid)
            {
                throw new DomainException(validationResult.ToString());
            }
        }

        public TimeSpan CalculateTotalWorkedHours(IEnumerable<Punch> punches)
        {
            var orderedPunches = punches.OrderBy(p => p.Timestamp).ToList();
            var totalWorkedHours = new TimeSpan();

            for (var i = 0; i < orderedPunches.Count; i += 2)
            {
                if (i + 1 < orderedPunches.Count)
                {
                    totalWorkedHours += orderedPunches[i + 1].Timestamp - orderedPunches[i].Timestamp;
                }
            }

            return totalWorkedHours;
        }
    }
}