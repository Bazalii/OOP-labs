using System;

namespace Banks.Tools
{
    public class DoubtfulAccountException : Exception
    {
        public DoubtfulAccountException(string message)
            : base(message)
        {
        }
    }
}