using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra
{
    public class StudyGroup : Group
    {
        private List<Lesson> _timetable = new ();

        public StudyGroup(string name)
            : base(name)
        {
        }

        public IReadOnlyList<Lesson> Timetable => _timetable;

        public void AddLesson(string time, int room)
        {
            _timetable.Add(new Lesson(time, room));
        }
    }
}