using System.Collections.Generic;
using System.Linq;

namespace Backups.FileSystem.Implementations
{
    public class MemoryDirectory : IDirectory
    {
        private readonly List<IStorageObject> _objects = new ();

        public MemoryDirectory(string pathToParentDirectory, string name)
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
            return PathToParentDirectory != string.Empty ? PathToParentDirectory + "\\" + Name : Name;
        }

        public override bool Equals(object obj)
        {
            return obj is MemoryDirectory memoryDirectory && Equals(memoryDirectory);
        }

        public override int GetHashCode()
        {
            return GetPath() != null ? GetPath().GetHashCode() : 0;
        }

        private bool Equals(MemoryDirectory other)
        {
            return GetPath() == other.GetPath();
        }
    }
}