using WorkTracker.Domain.Core;

namespace WorkTracker.Clock.Domain.Models;

public class Employee : Entity
{
    public string Email { get; private set; }
    public string Hash { get; private set; }

    public Employee(string email, string hash)
    {
        Email = email;
        Hash = hash;
    }
}