namespace Isu.Tools
{
    public class MaxStudentsPerGroupException : IsuException
    {
        public MaxStudentsPerGroupException(string message)
            : base(message)
        {
        }
    }
}