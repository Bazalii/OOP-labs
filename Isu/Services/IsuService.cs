using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private int _studentIds;

        private List<CourseNumber> _courseNumbers = new List<CourseNumber>()
        {
            new (1),
            new (2),
            new (3),
            new (4),
        };
        public Group AddGroup(string name)
        {
            var newGroup = new Group(name);
            int courseNumber = name[2] - '0';
            _courseNumbers[courseNumber].Groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group @group, string name)
        {
            var newStudent = new Student(_studentIds += 1, name, group.Name);
            group.AddStudent(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            foreach (CourseNumber course in _courseNumbers)
            {
                foreach (Group group in course.Groups)
                {
                    Student wantedStudent = group.GetStudent(id);
                    if (wantedStudent != null)
                    {
                        return wantedStudent;
                    }
                }
            }

            throw new IsuException($"Student with id:{id} doesn't exist");
        }

        public Student FindStudent(string name)
        {
            foreach (CourseNumber course in _courseNumbers)
            {
                foreach (Group group in course.Groups)
                {
                    Student wantedStudent = group.GetStudent(name);
                    if (wantedStudent != null)
                    {
                        return wantedStudent;
                    }
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            Group wantedGroup = FindGroup(groupName);
            if (wantedGroup != null)
            {
                return FindGroup(groupName).Students;
            }

            return new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Group> wantedGroups = FindGroups(courseNumber);
            if (!wantedGroups.Any())
            {
                return new List<Student>();
            }

            return wantedGroups.SelectMany(gr => gr.Students).ToList();
        }

        public Group FindGroup(string groupName)
        {
            foreach (CourseNumber course in _courseNumbers)
            {
                foreach (Group group in course.Groups)
                {
                    if (group.Name == groupName)
                    {
                        return group;
                    }
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return courseNumber.Groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            CourseNumber courseNumber = _courseNumbers[student.CurrentGroup[2] - '0'];
            foreach (Group group in courseNumber.Groups)
            {
                if (group.Students.Contains(student))
                {
                    group.Students.Remove(student);
                    newGroup.Students.Add(student);
                }
            }
        }
    }
}