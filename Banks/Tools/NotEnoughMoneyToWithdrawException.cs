using System;

namespace Banks.Tools
{
    public class NotEnoughMoneyToWithdrawException : Exception
    {
        public NotEnoughMoneyToWithdrawException(string message)
            : base(message)
        {
        }
    }
}