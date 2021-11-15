namespace Backups.FileSystem
{
    public interface IFile : IStorageObject
    {
        public void Write(byte[] information);

        public byte[] Read();
    }
}