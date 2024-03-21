namespace WorkTracker.Clock.UseCase.OutputViewModels
{
    public class MonthlyPunchesViewModel
    {
        public IEnumerable<DailyPunchesViewModel> DailyPunches { get; set; }

        public TimeSpan TotalMonthlyWorkedHours => GetMonthlyWorkedHours();

        private TimeSpan GetMonthlyWorkedHours()
        {
            TimeSpan totalWorkedHours = new();
            foreach (var dailyPunches in DailyPunches)
            {
                totalWorkedHours += dailyPunches.TotalWorkedHours;
            }
            return totalWorkedHours;
        }
    }
}