using System;

namespace IsuExtra.Tools
{
    public class NotExistException : Exception
    {
        public NotExistException(string message)
            : base(message)
        {
        }
    }
}