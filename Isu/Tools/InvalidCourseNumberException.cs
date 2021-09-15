namespace Isu.Tools
{
    public class InvalidCourseNumberException : IsuException
    {
        public InvalidCourseNumberException(string message)
            : base(message)
        {
        }
    }
}