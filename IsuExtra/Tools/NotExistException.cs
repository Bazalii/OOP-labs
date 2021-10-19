using System;

namespace IsuExtra
{
    public class NotExistException : Exception
    {
        public NotExistException(string message)
            : base(message)
        {
        }
    }
}