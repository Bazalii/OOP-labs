using System;
using System.Collections.Generic;
using Backups.FileSystem.Implementations;
using Reports.DAL.Entities;

namespace Reports.Server.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportDataBase _dataBase = new ReportDataBase(new WindowsFileSystem());

        public Report Create(Guid authorId, string commentary)
        {
            Report report = new Report(Guid.NewGuid(), authorId, commentary, ReportStatus.Draft, DateTime.Now);
            _dataBase.Add(report);
            return report;
        }

        public Report FindById(Guid id)
        {
            return _dataBase.FindById(id);
        }

        public List<Guid> GetAllFinishedDailyReports()
        {
            return _dataBase.GetAllFinishedDailyReports();
        }

        public Report Delete(Guid id)
        {
            return _dataBase.Delete(id);
        }

        public void ChangeStatus(Guid id, ReportStatus status)
        {
            _dataBase.ChangeStatus(id, status);
        }

        public void Update(Report report)
        {
            Delete(report.Id);
            _dataBase.Add(report);
        }

        public void Save()
        {
            _dataBase.Serialize();
        }

        public void Load()
        {
            _dataBase.DeSerialize();
        }
    }
}