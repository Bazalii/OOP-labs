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
    public class TaskDataBase : ITaskDataBase
    {
        private readonly IFileSystem _fileSystem;

        [JsonProperty]
        private List<Task> _tasks = new ();

        public TaskDataBase(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Add(Task task)
        {
            _tasks.Add(task);
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

        public Task ChangeTask(Guid employeeId, Guid taskId, string change)
        {
            Task task = FindById(taskId);
            task.AddChange(new Change(employeeId, DateTime.Now, change));
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

        public void Serialize()
        {
            _fileSystem.WriteToFile(PathsToFiles.TaskServicePath, Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(this, Formatting.Indented)));
        }

        public void DeSerialize()
        {
            _tasks = JsonConvert.DeserializeObject<TaskDataBase>(
                    Encoding.UTF8.GetString(_fileSystem.ReadFile(PathsToFiles.TaskServicePath)))
                ?._tasks;
        }
    }
}