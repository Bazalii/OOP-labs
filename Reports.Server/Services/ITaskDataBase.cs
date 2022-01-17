using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskDataBase : IDataBase
    {
        void Add(Task task);

        List<Task> GetAll();

        Task FindById(Guid id);

        Task GetByLatestTime();

        Task ChangeTask(Guid employeeId, Guid taskId, string change);

        List<Task> GetByEmployeeChanges(Guid id);

        void ChangeStatus(Guid id, TaskStatus status);

        List<Task> GetTasksForTheWeek();
    }
}