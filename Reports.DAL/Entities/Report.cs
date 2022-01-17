using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Reports.DAL.Entities
{
    public class Report
    {
        [JsonProperty]
        private List<Guid> _tasks = new ();

        public Report(Guid id, Guid authorId, string commentary, ReportStatus status, DateTime timeOfCreation)
        {
            Id = id;
            AuthorId = authorId;
            Commentary = commentary;
            Status = status;
            TimeOfCreation = timeOfCreation;
        }

        public Guid Id { get; }

        public Guid AuthorId { get; }

        public string Commentary { get; }

        public ReportStatus Status { get; }

        public DateTime TimeOfCreation { get; }

        public IReadOnlyList<Guid> Tasks => _tasks;

        public void AddTask(Guid task)
        {
            _tasks.Add(task);
        }
    }
}