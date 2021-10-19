using System;

namespace IsuExtra
{
    public class AlreadyEnrolledException : Exception
    {
        public AlreadyEnrolledException(string message)
            : base(message)
        {
        }
    }
}