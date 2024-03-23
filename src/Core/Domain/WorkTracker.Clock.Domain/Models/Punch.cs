using WorkTracker.Clock.Domain.Models.Enums;
using WorkTracker.Domain.Core;

namespace WorkTracker.Clock.Domain.Models
{
    public class Punch : Entity
    {
        public string EmployeeHash { get; private set; }
        public DateTime Timestamp { get; private set; }
        public DateTime? UpdatedTimestamp { get; private set; }
        public PunchType Type { get; private set; }
        public bool IsApproved { get; private set; }
        public Punch(PunchType type, string employeeHash)
        {
            Timestamp = DateTime.UtcNow;
            Type = type;
            EmployeeHash = employeeHash;
            IsApproved = true;
        }

        public void Update(DateTime updatedTimestamp)
        {
            UpdatedTimestamp = updatedTimestamp;
            IsApproved = false;
        }

        public void Approve()
        {
            IsApproved = true;
        }

        public DateTime GetTimestamp() 
        {
            if (UpdatedTimestamp != null && IsApproved)
            {
                return UpdatedTimestamp.Value;
            }
            else
            {
                return Timestamp;
            }
        }
    }
}