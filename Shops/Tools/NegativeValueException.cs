using System;

namespace Shops.Tools
{
    public class NegativeValueException : Exception
    {
        public NegativeValueException(string message)
            : base(message)
        {
        }
    }
}