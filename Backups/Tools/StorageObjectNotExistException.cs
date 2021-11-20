using System;

namespace Backups.Tools
{
    public class StorageObjectNotExistException : Exception
    {
        public StorageObjectNotExistException(string message)
            : base(message)
        {
        }
    }
}