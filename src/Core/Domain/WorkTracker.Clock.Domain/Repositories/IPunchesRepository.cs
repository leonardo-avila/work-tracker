using WorkTracker.Data.Core;
using WorkTracker.Clock.Domain.Models;

namespace WorkTracker.Clock.Domain.Repositories
{
	public interface IPunchesRepository : IRepository<Punch>
	{
		Task<IEnumerable<Punch>> GetPunches(string rm);
		Task<IEnumerable<Punch>> GetMonthlyPunches(string rm);
	}
}