namespace IsuExtra
{
    public class Lesson
    {
        public Lesson(string time, int room)
        {
            Time = time;
            Room = room;
        }

        public string Time { get; }

        public int Room { get; }
    }
}