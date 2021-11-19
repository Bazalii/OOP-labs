namespace Backups.FileSystem.Implementations
{
    public class MemoryFile : IFile
    {
        private byte[] _information;

        public MemoryFile(string name)
        {
            Name = name;
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
    }
}