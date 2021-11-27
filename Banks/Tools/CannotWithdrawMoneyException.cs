using System;

namespace Banks.Tools
{
    public class CannotWithdrawMoneyException : Exception
    {
        public CannotWithdrawMoneyException(string message)
            : base(message)
        {
        }
    }
}