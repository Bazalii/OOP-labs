namespace Backups.FileSystem
{
    public abstract class IFile : IStorageObject
    {
        public abstract void Write(byte[] information);

        public abstract byte[] Read();

        public abstract void SetName(string name);

        public abstract string GetName();
    }
}