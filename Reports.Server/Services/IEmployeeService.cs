﻿using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Employee Create(string name);

        List<Employee> GetAll();

        Employee FindByName(string name);

        Employee FindById(Guid id);

        IReadOnlyList<Task> GetEmployeeTasks(Guid id);

        Employee GetAssignedEmployee(Guid id);

        Employee ChangeAssignedEmployee(Guid employeeId, Guid taskId);

        List<Task> GetSubordinatesTasks(Guid id);

        Employee Delete(Guid id);

        Employee AddSubordinate(Guid supervisorId, Guid subordinateId);

        Employee Update(Employee entity);
    }
}