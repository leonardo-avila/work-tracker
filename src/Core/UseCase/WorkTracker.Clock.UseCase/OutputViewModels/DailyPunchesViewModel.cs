namespace WorkTracker.Clock.UseCase.OutputViewModels
{
    public class DailyPunchesViewModel
    {
        public TimeSpan TotalWorkedHours { get; set; }
        public IEnumerable<OutputPunchViewModel> Punches { get; set; }
    }
}