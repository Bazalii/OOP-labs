using System;
using System.Collections.Generic;
using Backups.FileSystem.Implementations;
using Reports.DAL.Entities;

namespace Reports.Server.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskDataBase _dataBase = new TaskDataBase(new WindowsFileSystem());

        public Task Create(string description)
        {
            Task task = new Task(Guid.NewGuid(), description, DateTime.Now, TaskStatus.Open);
            _dataBase.Add(task);
            return task;
        }

        public List<Task> GetAll()
        {
            return _dataBase.GetAll();
        }

        public Task FindById(Guid id)
        {
            return _dataBase.FindById(id);
        }

        public Task GetByLatestTime()
        {
            return _dataBase.GetByLatestTime();
        }

        public List<Task> GetByEmployeeChanges(Guid id)
        {
            return _dataBase.GetByEmployeeChanges(id);
        }

        public Task ChangeTask(Guid employeeId, Guid taskId, string change)
        {
            return _dataBase.ChangeTask(employeeId, taskId, change);
        }

        public void ChangeStatus(Guid id, TaskStatus status)
        {
            _dataBase.ChangeStatus(id, status);
        }

        public List<Task> GetTasksForTheWeek()
        {
            return _dataBase.GetTasksForTheWeek();
        }

        public void Save()
        {
            _dataBase.Serialize();
        }

        public void Load()
        {
            _dataBase.DeSerialize();
        }
    }
}