namespace Isu.Services
{
    public class Student
    {
        public Student(int id, string name, string groupName)
        {
            Id = id;
            Name = name;
            CurrentGroup = groupName;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string CurrentGroup { get; private set; }
    }
}