using System.Collections.Generic;

namespace Backups.FileSystem.Implementations
{
    public class MemoryArchive : IDirectory
    {
        private readonly List<IStorageObject> _objects = new ();

        public MemoryArchive(string pathToParentDirectory, string name)
        {
            PathToParentDirectory = pathToParentDirectory;
            Name = name;
        }

        public override IReadOnlyList<IStorageObject> GetObjects()
        {
            return _objects;
        }

        public override void AddObject(IStorageObject storageObject)
        {
            _objects.Add(storageObject);
        }

        public void RemoveObject(IStorageObject storageObject)
        {
            _objects.Remove(storageObject);
        }

        public override void SetPath(string path)
        {
            PathToParentDirectory = path;
        }

        public override string GetPath()
        {
            return PathToParentDirectory + "\\" + Name;
        }

        public override bool Equals(object obj)
        {
            return obj is MemoryArchive memoryArchive && Equals(memoryArchive);
        }

        public override int GetHashCode()
        {
            return GetPath() != null ? GetPath().GetHashCode() : 0;
        }

        private bool Equals(MemoryArchive other)
        {
            return GetPath() == other.GetPath();
        }
    }
}