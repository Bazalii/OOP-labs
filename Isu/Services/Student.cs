namespace Isu.Services
{
    public class Student
    {
        public Student(int id, string name)
        {
            this.Id = id;
            Name = name;
        }

        public int Id
        {
            get;
        }

        public string Name
        {
            get;
        }
    }
}