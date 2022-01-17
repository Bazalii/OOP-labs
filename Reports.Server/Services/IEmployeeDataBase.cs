using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeDataBase : IDataBase
    {
        public void Add(Employee employee);

        public Employee FindByName(string name);

        public Employee FindById(Guid id);

        public List<Employee> GetAll();

        public Employee GetAssignedEmployee(Guid id);

        public Employee Delete(Guid id);
    }
}