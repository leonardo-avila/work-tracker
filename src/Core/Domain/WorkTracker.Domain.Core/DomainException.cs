namespace WorkTracker.Domain.Core
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        { }
    }
}