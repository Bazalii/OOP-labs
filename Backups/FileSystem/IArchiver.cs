namespace Backups.FileSystem
{
    public interface IArchiver
    {
        byte[] Compress(byte[] file);

        byte[] Decompress(byte[] file);
    }
}