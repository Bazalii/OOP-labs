using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportDataBase : IDataBase
    {
        void Add(Report report);

        Report FindById(Guid id);

        void ChangeStatus(Guid id, ReportStatus status);

        List<Guid> GetAllFinishedDailyReports();

        Report Delete(Guid id);
    }
}