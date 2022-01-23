using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeDataBase : IDataBase
    {
        void Add(Employee employee);

        Employee FindByName(string name);

        Employee FindById(Guid id);

        List<Employee> GetAll();

        Employee GetAssignedEmployee(Guid id);

        Employee Delete(Guid id);
    }
}