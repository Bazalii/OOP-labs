namespace Isu.Tools
{
    public class InvalidGroupNameException : IsuException
    {
        public InvalidGroupNameException(string message)
            : base(message)
        {
        }
    }
}