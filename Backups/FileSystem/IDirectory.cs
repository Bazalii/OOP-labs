using System.Collections.Generic;

namespace Backups.FileSystem
{
    public interface IDirectory : IStorageObject
    {
        public IReadOnlyList<IStorageObject> GetObjects();
        public void AddObject(IStorageObject obj);
    }
}