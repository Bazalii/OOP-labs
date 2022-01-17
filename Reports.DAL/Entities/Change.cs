using System;

namespace Reports.DAL.Entities
{
    public class Change
    {
        public Change(Guid employeeId, DateTime time, string commentary)
        {
            if (EmployeeId == Guid.Empty) throw new ArgumentException("Id cannot be empty!", nameof(employeeId));
            if (string.IsNullOrWhiteSpace(commentary)) throw new ArgumentNullException(nameof(commentary), "Commentary is invalid!");
            EmployeeId = employeeId;
            Time = time;
            Commentary = commentary;
        }

        public Guid EmployeeId { get; }

        public DateTime Time { get; }

        public string Commentary { get; }
    }
}