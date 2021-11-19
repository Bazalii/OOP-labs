using System.Collections.Generic;

namespace Backups.FileSystem
{
    public abstract class IDirectory : IStorageObject
    {
        public abstract IReadOnlyList<IStorageObject> GetObjects();
        public abstract void AddObject(IStorageObject storageObject);
    }
}