using WorkTracker.Clock.UseCase.OutputViewModels;

namespace WorkTracker.Clock.UseCase.Ports
{
	public interface IPunchUseCases
	{
		Task<DailyPunchesViewModel> GetPunches(string rm);
		Task<OutputPunchViewModel> Punch(string rm);
		Task<MonthlyPunchesViewModel> GetMonthlyPunches(string rm);
	}
}