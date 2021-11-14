using System;

namespace IsuExtra.Tools
{
    public class AlreadyEnrolledException : Exception
    {
        public AlreadyEnrolledException(string message)
            : base(message)
        {
        }
    }
}