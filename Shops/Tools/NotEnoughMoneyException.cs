using System;

namespace Shops.Tools
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(string message)
            : base(message)
        {
        }
    }
}