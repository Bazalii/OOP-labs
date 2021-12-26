using System;

namespace Banks.Tools
{
    public class TheSameAccountsException : Exception
    {
        public TheSameAccountsException(string message)
            : base(message)
        {
        }
    }
}