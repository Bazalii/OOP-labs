using System.Collections.Generic;

namespace Isu.Services
{
    public class Group
    {
        public List<Student> Students = new List<Student>();
        private string _name;

        public Group(string name)
        {
            _name = name;
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
    }
}