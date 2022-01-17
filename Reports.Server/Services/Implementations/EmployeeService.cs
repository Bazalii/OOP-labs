using System;
using System.Collections.Generic;
using Backups.FileSystem.Implementations;
using Reports.DAL.Entities;

namespace Reports.Server.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeDataBase _dataBase = new EmployeeDataBase(new WindowsFileSystem());

        public Employee Create(string name)
        {
            Employee employee = new Employee(Guid.NewGuid(), name);
            _dataBase.Add(employee);
            return employee;
        }

        public List<Employee> GetAll()
        {
            return _dataBase.GetAll();
        }

        public Employee FindByName(string name)
        {
            return _dataBase.FindByName(name);
        }

        public Employee FindById(Guid id)
        {
            return _dataBase.FindById(id);
        }

        public IReadOnlyList<Guid> GetEmployeeTasks(Guid id)
        {
            return FindById(id).Tasks;
        }

        public Employee AssignEmployee(Guid employeeId, Guid taskId)
        {
            Employee employee = FindById(employeeId);
            employee.AddTask(taskId);
            return employee;
        }

        public Employee GetAssignedEmployee(Guid id)
        {
            return _dataBase.GetAssignedEmployee(id);
        }

        public Employee ChangeAssignedEmployee(Guid employeeId, Guid taskId)
        {
            GetAssignedEmployee(taskId).RemoveTask(taskId);
            Employee employee = FindById(employeeId);
            employee.AddTask(taskId);
            return employee;
        }

        public List<Guid> GetSubordinatesTasks(Guid id)
        {
            List<Guid> output = new ();
            foreach (Employee employee in FindById(id).Subordinates)
            {
                output.AddRange(employee.Tasks);
            }

            return output;
        }

        public Employee Delete(Guid id)
        {
            return _dataBase.Delete(id);
        }

        public Employee AddSubordinate(Guid supervisorId, Guid subordinateId)
        {
            Employee supervisor = FindById(supervisorId);
            supervisor.AddSubordinate(FindById(subordinateId));
            return supervisor;
        }

        public Employee Update(Employee entity)
        {
            return null;
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