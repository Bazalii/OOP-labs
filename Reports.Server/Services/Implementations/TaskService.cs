using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL.Entities;

namespace Reports.Server.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private List<Task> _tasks = new ();

        public Task Create(string description)
        {
            Task task = new Task(Guid.NewGuid(), description, DateTime.Now, TaskStatus.Open);
            _tasks.Add(task);
            return task;
        }

        public List<Task> GetAll()
        {
            return _tasks;
        }

        public Task FindById(Guid id)
        {
            return _tasks.FirstOrDefault(task => task.Id == id);
        }

        public Task GetByLatestTime()
        {
            if (!_tasks.Any()) return null;
            Console.WriteLine(_tasks.Count);
            Task task = _tasks[0];
            foreach (Task currentTask in _tasks)
            {
                if (currentTask.TimeOfCreation.CompareTo(task.TimeOfCreation) > 0)
                {
                    task = currentTask;
                }
            }

            return task;
        }

        public List<Task> GetByEmployeeChanges(Guid id)
        {
            return _tasks.Where(task => task.Changes.FirstOrDefault(change => change.EmployeeId == id) != null).ToList();
        }

        public void ChangeStatus(Guid id, TaskStatus status)
        {
            FindById(id).ChangeStatus(status);
        }

        public List<Task> GetTasksForTheWeek()
        {
            return _tasks.Where(task => DateTime.Now.Subtract(task.TimeOfCreation) < TimeSpan.FromDays(7)).ToList();
        }
    }
}