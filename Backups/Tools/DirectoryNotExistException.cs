using System;

namespace Backups.Tools
{
    public class DirectoryNotExistException : Exception
    {
        public DirectoryNotExistException(string message)
            : base(message)
        {
        }
    }
}