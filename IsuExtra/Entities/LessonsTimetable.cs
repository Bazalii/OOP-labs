using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Entities
{
    public class LessonsTimetable
    {
        private readonly List<Lesson> _lessons = new ();
        public IReadOnlyList<Lesson> Lessons => _lessons;

        public void AddLesson(Lesson lesson)
        {
            _lessons.Add(lesson);
        }

        public bool IntersectsWith(LessonsTimetable otherTimetable)
        {
            return _lessons.FirstOrDefault(streamLesson =>
                otherTimetable.Lessons.FirstOrDefault(studyLesson => studyLesson.StartTime == streamLesson.StartTime) !=
                null) != null;
        }
    }
}