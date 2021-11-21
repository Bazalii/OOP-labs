using System.Collections.Generic;

namespace Backups.FileSystem
{
    public abstract class VirtualDirectory : StorageObject
    {
        public abstract IReadOnlyList<StorageObject> GetObjects();
        public abstract void AddObject(StorageObject storageObject);
    }
}