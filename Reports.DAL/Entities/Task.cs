﻿using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class Task
    {
        private List<Change> _changes = new ();

        public Task(Guid id, string description, DateTime timeOfCreation, TaskStatus status)
        {
            Id = id;
            Description = description;
            TimeOfCreation = timeOfCreation;
            Status = status;
        }

        public Guid Id { get; }

        public string Description { get; set; }

        public DateTime TimeOfCreation { get; }

        public TaskStatus Status { get; set; }

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