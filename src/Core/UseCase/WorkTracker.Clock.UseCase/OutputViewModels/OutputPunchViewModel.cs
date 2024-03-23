namespace WorkTracker.Clock.UseCase.OutputViewModels
{
    public class OutputPunchViewModel
    {
        public DateTime Timestamp { get; set; }
        public string PunchType { get; set; }
        public bool IsApproved { get; set; }
    }
}