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
            if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty!", nameof(id));
            if (authorId == Guid.Empty) throw new ArgumentException("Id cannot be empty!", nameof(authorId));
            if (string.IsNullOrWhiteSpace(commentary)) throw new ArgumentNullException(nameof(commentary), "Commentary is invalid!");
            Id = id;
            AuthorId = authorId;
            Commentary = commentary;
            Status = status;
            TimeOfCreation = timeOfCreation;
        }

        public Guid Id { get; }

        public Guid AuthorId { get; }

        public string Commentary { get; }

        public ReportStatus Status { get; private set; }

        public DateTime TimeOfCreation { get; }

        public IReadOnlyList<Guid> Tasks => _tasks;

        public void AddTask(Guid task)
        {
            _tasks.Add(task);
        }

        public void ChangeStatus(ReportStatus status)
        {
            Status = status;
        }
    }
}