namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(string time, int room, string teacher)
        {
            Time = time;
            Room = room;
            Teacher = teacher;
        }

        public string Time { get; }

        public int Room { get; }

        public string Teacher { get; }
    }
}