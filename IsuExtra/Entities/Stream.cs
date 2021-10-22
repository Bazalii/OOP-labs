using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class Stream
    {
        private const int MaxStudents = 4;

        private readonly List<Lesson> _timetable = new ();

        private readonly List<Student> _students = new ();

        public Stream(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null!");
        }

        public string Name { get; }
        public IReadOnlyList<Lesson> Timetable => _timetable;

        public IReadOnlyList<Student> Students => _students;

        public void AddLesson(string time, int room, string teacher)
        {
            _timetable.Add(new Lesson(time, room, teacher));
        }

        public void AddStudent(Student student)
        {
            if (_students.Count == MaxStudents)
            {
                throw new MaxStudentsPerGroupException($"Maximum number({MaxStudents}) of students per group is reached");
            }

            _students.Add(student);
        }

        public Student GetStudent(int id)
        {
            return _students.FirstOrDefault(student => student.Id == id);
        }

        public void RemoveStudent(int id)
        {
            _students.Remove(_students.Find(student => student.Id == id));
        }
    }
}