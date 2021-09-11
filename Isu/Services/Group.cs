using System.Collections.Generic;
using System.Linq;

namespace Isu.Services
{
    public class Group
    {
        public Group(string name)
        {
            Name = name;
        }

        public List<Student> Students { get; private set; }
        public string Name { get; private set; }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public Student GetStudent(string studentName)
        {
            foreach (Student student in Students)
            {
                if (student.Name == studentName)
                {
                    return student;
                }
            }

            return null;
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in Students)
            {
                if (student.Id == id)
                {
                    return student;
                }
            }

            return null;
        }
    }
}