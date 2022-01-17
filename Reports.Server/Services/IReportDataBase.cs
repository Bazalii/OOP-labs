using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportDataBase : IDataBase
    {
        public void Add(Report report);

        public Report FindById(Guid id);

        public List<Guid> GetAllFinishedDailyReports();

        public Report Delete(Guid id);
    }
}