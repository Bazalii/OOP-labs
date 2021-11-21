using System;
using System.Collections.Generic;

namespace Backups.FileSystem.Implementations
{
    public class MemoryArchive : VirtualDirectory
    {
        private readonly List<StorageObject> _objects = new ();

        public MemoryArchive(string pathToParentDirectory, string name)
        {
            PathToParentDirectory = pathToParentDirectory ??
                                    throw new ArgumentNullException(
                                        nameof(pathToParentDirectory), "Path cannot be null!");
            Name = name ??
                   throw new ArgumentNullException(
                       nameof(name), "Name cannot be null!");
        }

        public override IReadOnlyList<StorageObject> GetObjects()
        {
            return _objects;
        }

        public override void AddObject(StorageObject storageObject)
        {
            _objects.Add(storageObject);
        }

        public void RemoveObject(StorageObject storageObject)
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