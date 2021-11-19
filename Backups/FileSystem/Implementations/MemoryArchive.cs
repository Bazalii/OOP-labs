using System.Collections.Generic;

namespace Backups.FileSystem.Implementations
{
    public class MemoryArchive : IDirectory
    {
        private readonly List<IStorageObject> _objects = new ();

        public MemoryArchive(string pathToParentDirectory)
        {
            PathToParentDirectory = pathToParentDirectory;
        }

        public override IReadOnlyList<IStorageObject> GetObjects()
        {
            return _objects;
        }

        public override void AddObject(IStorageObject storageObject)
        {
            _objects.Add(storageObject);
        }

        public override void SetPath(string path)
        {
            PathToParentDirectory = path;
        }

        public override string GetPath()
        {
            return PathToParentDirectory;
        }
    }
}