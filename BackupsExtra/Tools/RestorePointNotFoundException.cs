using System;

namespace BackupsExtra.Tools
{
    public class RestorePointNotFoundException : Exception
    {
        public RestorePointNotFoundException(string message)
            : base(message)
        {
        }
    }
}