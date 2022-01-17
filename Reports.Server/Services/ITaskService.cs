﻿using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        Task Create(string description);

        List<Task> GetAll();

        Task FindById(Guid id);

        Task GetByLatestTime();

        public List<Task> GetByEmployeeChanges(Guid id);

        public Task ChangeTask(Guid employeeId, Guid taskId, string change);

        void ChangeStatus(Guid id, TaskStatus status);

        List<Task> GetTasksForTheWeek();

        public void Save();

        public void Load();
    }
}