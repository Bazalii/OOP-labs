using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Entities
{
    public class StudyGroup : Group
    {
        private readonly List<Lesson> _timetable = new ();

        public StudyGroup(string name)
            : base(name)
        {
        }

        public IReadOnlyList<Lesson> Timetable => _timetable;

        public void AddLesson(Lesson lesson)
        {
            _timetable.Add(lesson);
        }
    }
}