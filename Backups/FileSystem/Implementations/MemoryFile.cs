using System;

namespace Backups.FileSystem.Implementations
{
    public class MemoryFile : VirtualFile
    {
        private byte[] _information;

        public MemoryFile(string pathToParentDirectory, string name)
        {
            PathToParentDirectory = pathToParentDirectory ??
                                    throw new ArgumentNullException(
                                        nameof(pathToParentDirectory), "Path cannot be null!");
            Name = name ??
                   throw new ArgumentNullException(
                       nameof(name), "Name cannot be null!");
        }

        public override void Write(byte[] information)
        {
            _information = information;
        }

        public override byte[] Read()
        {
            return _information;
        }

        public override void SetName(string name)
        {
            Name = name;
        }

        public override string GetName()
        {
            return Name;
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
            return obj is MemoryFile memoryFile && Equals(memoryFile);
        }

        public override int GetHashCode()
        {
            return GetPath() != null ? GetPath().GetHashCode() : 0;
        }

        private bool Equals(MemoryFile other)
        {
            return GetPath() == other.GetPath();
        }
    }
}