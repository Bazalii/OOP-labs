using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class Report
    {
        private List<Task> _tasks = new ();

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

        public IReadOnlyList<Task> Tasks => _tasks;

        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }
    }
}