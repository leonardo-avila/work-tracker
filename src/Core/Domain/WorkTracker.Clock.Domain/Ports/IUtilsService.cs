using WorkTracker.Clock.Domain.Models;

namespace WorkTracker.Clock.Domain.Ports
{
    public interface IUtilsService
    {
        string GenerateHash(string rm);
    }
}