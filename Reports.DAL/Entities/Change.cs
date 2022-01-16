using System;

namespace Reports.DAL.Entities
{
    public class Change
    {
        public Change(Guid employeeId, DateTime time, string commentary)
        {
            EmployeeId = employeeId;
            Time = time;
            Commentary = commentary;
        }

        public Guid EmployeeId { get; }

        public DateTime Time { get; }

        public string Commentary { get; }
    }
}