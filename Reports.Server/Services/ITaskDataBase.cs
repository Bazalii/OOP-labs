using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskDataBase : IDataBase
    {
        public void Add(Task task);

        public List<Task> GetAll();

        public Task FindById(Guid id);

        public Task GetByLatestTime();

        public Task ChangeTask(Guid employeeId, Guid taskId, string change);

        public List<Task> GetByEmployeeChanges(Guid id);

        public void ChangeStatus(Guid id, TaskStatus status);

        public List<Task> GetTasksForTheWeek();
    }
}