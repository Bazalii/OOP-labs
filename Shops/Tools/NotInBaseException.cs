using System;

namespace Shops.Tools
{
    public class NotInBaseException : Exception
    {
        public NotInBaseException(string message)
            : base(message)
        {
        }
    }
}