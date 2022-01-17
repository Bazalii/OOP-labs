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
    public class EmployeeDataBase : IEmployeeDataBase
    {
        private IFileSystem _fileSystem = new WindowsFileSystem();

        [JsonProperty]
        private List<Employee> _employees = new ();

        public EmployeeDataBase(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        public Employee FindByName(string name)
        {
            return _employees.FirstOrDefault(employee => employee.Name == name);
        }

        public Employee FindById(Guid id)
        {
            return _employees.FirstOrDefault(employee => employee.Id == id);
        }

        public List<Employee> GetAll()
        {
            return _employees;
        }

        public Employee GetAssignedEmployee(Guid id)
        {
            return _employees.FirstOrDefault(employee => employee.Tasks.Contains(id));
        }

        public Employee Delete(Guid id)
        {
            Employee employee = FindById(id);
            _employees.Remove(employee);
            return employee;
        }

        public void Serialize()
        {
            _fileSystem.WriteToFile(PathsToFiles.EmployeeServicePath, Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(this, Formatting.Indented)));
        }

        public void DeSerialize()
        {
            _employees = JsonConvert.DeserializeObject<EmployeeDataBase>(
                    Encoding.UTF8.GetString(_fileSystem.ReadFile(PathsToFiles.EmployeeServicePath)))
                ?._employees;
        }
    }
}