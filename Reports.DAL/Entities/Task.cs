using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Reports.DAL.Entities
{
    public class Task
    {
        [JsonProperty]
        private List<Change> _changes = new ();

        public Task(Guid id, string description, DateTime timeOfCreation, TaskStatus status)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty!", nameof(id));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException(nameof(description), "Description is invalid!");
            Id = id;
            Description = description;
            TimeOfCreation = timeOfCreation;
            Status = status;
        }

        public Guid Id { get; }

        public string Description { get; set; }

        public DateTime TimeOfCreation { get; }

        public TaskStatus Status { get; private set; }

        public IReadOnlyList<Change> Changes => _changes;

        public void ChangeStatus(TaskStatus taskStatus)
        {
            Status = taskStatus;
        }

        public void AddChange(Change change)
        {
            _changes.Add(change);
        }

        public override bool Equals(object obj)
        {
            return obj is Task task && Equals(task);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private bool Equals(Task other)
        {
            return Id == other.Id && TimeOfCreation == other.TimeOfCreation && Status == other.Status;
        }
    }
}