using System;

namespace IsuExtra.Tools
{
    public class NoAvailableStreamsException : Exception
    {
        public NoAvailableStreamsException(string message)
            : base(message)
        {
        }
    }
}