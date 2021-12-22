using System;

namespace Backups.Tools
{
    public class StorageObjectAlreadyExists : Exception
    {
        public StorageObjectAlreadyExists(string message)
            : base(message)
        {
        }
    }
}