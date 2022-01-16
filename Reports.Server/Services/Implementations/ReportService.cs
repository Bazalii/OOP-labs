using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL.Entities;

namespace Reports.Server.Services.Implementations
{
    public class ReportService : IReportService
    {
        private List<Report> _reports = new ();

        public Report Create(Guid authorId, string commentary)
        {
            Report report = new Report(Guid.NewGuid(), authorId, commentary, ReportStatus.Draft, DateTime.Now);
            _reports.Add(report);
            return report;
        }

        public Report FindById(Guid id)
        {
            return _reports.FirstOrDefault(report => report.Id == id);
        }

        public List<Guid> GetAllFinishedDailyReports()
        {
            List<Guid> employees = new ();
            foreach (Report report in _reports)
            {
                if (DateTime.Now.Subtract(report.TimeOfCreation) < TimeSpan.FromDays(1))
                {
                    employees.Add(report.AuthorId);
                }
            }

            return employees;
        }
    }
}