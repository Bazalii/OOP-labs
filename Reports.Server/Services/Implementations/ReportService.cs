using System;
using System.Collections.Generic;
using System.Linq;
using Backups.FileSystem.Implementations;
using Newtonsoft.Json;
using Reports.DAL.Entities;

namespace Reports.Server.Services.Implementations
{
    public class ReportService : IReportService
    {
        private IReportDataBase _dataBase = new ReportDataBase(new WindowsFileSystem());

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