using System.IO;
using System.IO.Compression;

namespace Backups.FileSystem.Implementations
{
    public class ZipArchiver : IArchiver
    {
        public byte[] Compress(byte[] file)
        {
            MemoryStream fileStream = new (file);
            MemoryStream memoryStream = new ();
            GZipStream gZipStream = new (memoryStream, CompressionLevel.Optimal);
            fileStream.CopyTo(gZipStream);
            gZipStream.Flush();
            return memoryStream.ToArray();
        }

        public byte[] Decompress(byte[] file)
        {
            MemoryStream fileStream = new (file);
            MemoryStream memoryStream = new ();
            GZipStream gZipStream = new (fileStream, CompressionMode.Decompress);
            gZipStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}