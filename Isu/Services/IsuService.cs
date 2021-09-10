using System.Collections.Generic;
using System.Linq;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private int studentIds = 0;
        private List<Group> _groups = new List<Group>();

        public Group AddGroup(string name)
        {
            var newGroup = new Group(name);
            _groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group @group, string name)
        {
            var newStudent = new Student(studentIds += 1, name);
            group.AddStudent(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            foreach (var group in _groups)
            {
                foreach (var student in group.Students)
                {
                    if (student.Id == id)
                    {
                        return student;
                    }
                }
            }

            return null;
        }

        public Student FindStudent(string name)
        {
            foreach (var group in _groups)
            {
                foreach (var student in group.Students)
                {
                    if (student.Name == name)
                    {
                        return student;
                    }
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            throw new System.NotImplementedException();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            throw new System.NotImplementedException();
        }

        public Group FindGroup(string groupName)
        {
            throw new System.NotImplementedException();
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            throw new System.NotImplementedException();
        }
    }
}