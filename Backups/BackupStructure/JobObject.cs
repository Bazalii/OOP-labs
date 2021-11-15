using System;

namespace Backups.BackupStructure
{
    public class JobObject
    {
        public JobObject(string pathToFile)
        {
            PathToFile = pathToFile ??
                         throw new ArgumentNullException(
                             nameof(pathToFile), "PathToFile cannot be null!");
        }

        public string PathToFile { get; set; }
    }
}