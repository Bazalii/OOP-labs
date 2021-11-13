using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Entities
{
    public class StudyGroup : Group
    {
        public StudyGroup(string name)
            : base(name)
        {
        }

        public LessonsTimetable Timetable { get; } = new ();

        public void AddLesson(Lesson lesson)
        {
            Timetable.AddLesson(lesson);
        }
    }
}