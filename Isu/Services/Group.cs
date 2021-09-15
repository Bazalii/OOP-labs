using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        private const int MaxStudents = 5;

        public Group(string name)
        {
            if (name[0] != 'M' || name[1] != '3' || name[2] - '0' < 1 || name[2] - '0' > 4 || name[3] - '0' < 0 ||
                name[3] - '0' > 9 || name[4] - '0' < 0 || name[4] - '0' > 9)
            {
                throw new InvalidGroupNameException("Incorrect group name");
            }

            Name = name;
        }

        public List<Student> Students { get; private set; } = new List<Student>();
        public string Name { get; private set; }

        public void AddStudent(Student student)
        {
            if (Students.Count == MaxStudents)
            {
                throw new MaxStudentsPerGroupException($"Maximum number({MaxStudents}) of students per group is reached");
            }

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