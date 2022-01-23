using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backups.FileSystem;
using Backups.FileSystem.Implementations;
using Newtonsoft.Json;
using Reports.DAL.Entities;

namespace Reports.Server.Services.Implementations
{
    public class ReportDataBase : IReportDataBase
    {
        private readonly IFileSystem _fileSystem;

        [JsonProperty]
        private List<Report> _reports = new ();

        public ReportDataBase(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Add(Report report)
        {
            _reports.Add(report);
        }

        public Report FindById(Guid id)
        {
            return _reports.FirstOrDefault(employee => employee.Id == id);
        }

        public void ChangeStatus(Guid id, ReportStatus status)
        {
            FindById(id).ChangeStatus(status);
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

        public Report Delete(Guid id)
        {
            Report report = FindById(id);
            _reports.Remove(report);
            return report;
        }

        public void Serialize()
        {
            _fileSystem.WriteToFile(PathsToFiles.ReportServicePath, Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(this, Formatting.Indented)));
        }

        public void DeSerialize()
        {
            _reports = JsonConvert.DeserializeObject<ReportDataBase>(
                    Encoding.UTF8.GetString(_fileSystem.ReadFile(PathsToFiles.ReportServicePath)))
                ?._reports;
        }
    }
}