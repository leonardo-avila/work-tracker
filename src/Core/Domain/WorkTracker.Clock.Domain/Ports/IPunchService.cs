using WorkTracker.Clock.Domain.Models;

namespace WorkTracker.Clock.Domain.Ports
{
	public interface IPunchService
	{
		bool IsValidType(string type);
		void ValidatePunch(Punch punch);
		TimeSpan CalculateTotalWorkedHours(IEnumerable<Punch> punches);
	}
}