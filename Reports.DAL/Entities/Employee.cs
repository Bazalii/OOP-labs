using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        [JsonProperty]
        private List<Employee> _subordinates = new ();

        [JsonProperty]
        private List<Guid> _tasks = new ();

        public Employee(Guid id, string name)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is invalid");
            }

            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }

        public IReadOnlyList<Employee> Subordinates => _subordinates;

        public IReadOnlyList<Guid> Tasks => _tasks;

        public void AddSubordinate(Employee subordinate)
        {
            _subordinates.Add(subordinate);
        }

        public void AddTask(Guid task)
        {
            _tasks.Add(task);
        }

        public void RemoveTask(Guid task)
        {
            _tasks.Remove(task);
        }

        public Guid GetTaskById(Guid id)
        {
            return _tasks.FirstOrDefault(taskId => taskId == id);
        }

        public override bool Equals(object obj)
        {
            return obj is Employee employee && Equals(employee);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private bool Equals(Employee other)
        {
            return Id == other.Id && Name == other.Name;
        }
    }
}