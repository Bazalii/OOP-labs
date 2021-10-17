using System;
using System.Collections.Generic;

namespace IsuExtra
{
    public class Stream
    {
        private List<Lesson> _timetable = new ();

        public Stream(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null!");
        }

        public string Name { get; }
        public IReadOnlyList<Lesson> Timetable { get; set; }

        public void AddLesson(string time, int room)
        {
            _timetable.Add(new Lesson(time, room));
            Timetable = _timetable;
        }
    }
}