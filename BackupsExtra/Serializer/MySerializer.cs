using System;
using System.Text;
using Backups.FileSystem;
using BackupsExtra.BackupExtraStructure;
using Newtonsoft.Json;

namespace BackupsExtra.Serializer
{
    public class MySerializer
    {
        private string _pathToFile;

        private IFileSystem _fileSystem;

        public MySerializer(string pathToFile, IFileSystem fileSystem)
        {
            _pathToFile = pathToFile ??
                          throw new ArgumentNullException(
                              nameof(pathToFile), "Path to file cannot be null!");
            _fileSystem = fileSystem ??
                          throw new ArgumentNullException(
                              nameof(fileSystem), "FileSystem cannot be null!");
        }

        public void Serialize(ModifiedBackupJob modifiedBackupJob)
        {
            Console.WriteLine(JsonConvert.SerializeObject(modifiedBackupJob, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            }));
            _fileSystem.WriteToFile(_pathToFile, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(modifiedBackupJob, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            })));
        }

        public ModifiedBackupJob DeSerialize()
        {
            return JsonConvert.DeserializeObject<ModifiedBackupJob>(Encoding.UTF8.GetString(_fileSystem.ReadFile(_pathToFile)), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
        }
    }
}