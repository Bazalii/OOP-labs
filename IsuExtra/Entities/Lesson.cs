using System;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(DateTime startTime, DateTime endTime, int room, string teacher)
        {
            StartTime = startTime;
            EndTime = endTime;
            Room = room;
            Teacher = teacher;
        }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public int Room { get; }

        public string Teacher { get; }
    }
}