using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Report Create(Guid authorId, string commentary);

        Report FindById(Guid id);

        public List<Guid> GetAllFinishedDailyReports();

        public Report Delete(Guid id);

        void Update(Report report);

        public void Save();

        public void Load();
    }
}