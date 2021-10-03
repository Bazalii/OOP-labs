using System;

namespace Shops.Tools
{
    public class NullValueException : Exception
    {
        public NullValueException(string message)
            : base(message)
        {
        }
    }
}