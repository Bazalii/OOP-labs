using System;

namespace Shops.Tools
{
    public class NotEnoughProductException : Exception
    {
        public NotEnoughProductException(string message)
            : base(message)
        {
        }
    }
}