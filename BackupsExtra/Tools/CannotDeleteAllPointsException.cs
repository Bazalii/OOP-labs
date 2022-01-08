using System;

namespace BackupsExtra.Tools
{
    public class CannotDeleteAllPointsException : Exception
    {
        public CannotDeleteAllPointsException(string message)
            : base(message)
        {
        }
    }
}