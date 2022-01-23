using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Report Create(Guid authorId, string commentary);

        Report FindById(Guid id);

        List<Guid> GetAllFinishedDailyReports();

        Report Delete(Guid id);

        void ChangeStatus(Guid id, ReportStatus status);

        void Update(Report report);

        void Save();

        void Load();
    }
}