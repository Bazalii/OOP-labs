using System;

namespace IsuExtra
{
    public class NoAvailableStreamsException : Exception
    {
        public NoAvailableStreamsException(string message)
            : base(message)
        {
        }
    }
}