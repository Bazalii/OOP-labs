using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL.Entities;

namespace Reports.Server.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private List<Employee> _employees = new ();

        public Employee Create(string name)
        {
            Employee employee = new Employee(Guid.NewGuid(), name);
            _employees.Add(employee);
            Console.WriteLine(_employees.Count);
            return employee;
        }

        public List<Employee> GetAll()
        {
            return _employees;
        }

        public Employee FindByName(string name)
        {
            Console.WriteLine(_employees.Count);
            Console.WriteLine(name);
            return _employees.FirstOrDefault(employee => employee.Name == name);
        }

        public Employee FindById(Guid id)
        {
            return _employees.FirstOrDefault(employee => employee.Id == id);
        }

        public IReadOnlyList<Task> GetEmployeeTasks(Guid id)
        {
            return FindById(id).Tasks;
        }

        public Employee GetAssignedEmployee(Guid id)
        {
            return _employees.FirstOrDefault(employee => employee.Tasks.FirstOrDefault(task => task.Id == id) != null);
        }

        public Employee AssignEmployee(Guid employeeId)
        {
            FindById(employeeId).AddTask();
        }

        public Employee ChangeAssignedEmployee(Guid employeeId, Guid taskId)
        {
            Task task = GetAssignedEmployee(taskId).GetTaskById(taskId);
            GetAssignedEmployee(taskId).RemoveTask(task);
            Employee employee = FindById(employeeId);
            employee.AddTask(task);
            return employee;
        }

        public List<Task> GetSubordinatesTasks(Guid id)
        {
            List<Task> output = new ();
            foreach (Employee employee in FindById(id).Subordinates)
            {
                output.AddRange(employee.Tasks);
            }

            return output;
        }

        public Employee Delete(Guid id)
        {
            Employee employee = FindById(id);
            _employees.Remove(employee);
            return employee;
        }

        public Employee AddSubordinate(Guid supervisorId, Guid subordinateId)
        {
            Employee supervisor = FindById(supervisorId);
            Console.WriteLine(supervisor.Subordinates.Count);
            supervisor.AddSubordinate(FindById(subordinateId));
            Console.WriteLine(supervisor.Subordinates.Count);
            return supervisor;
        }

        public Employee Update(Employee entity)
        {
        }
    }
}