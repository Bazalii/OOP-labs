using System;

namespace Backups.BackupStructure
{
    public class JobObject
    {
        public JobObject(string pathToFile)
        {
            PathToFile = pathToFile ??
                         throw new ArgumentNullException(
                             nameof(pathToFile), "Path cannot be null!");
        }

        public string PathToFile { get; }

        public override bool Equals(object obj)
        {
            return obj is JobObject jobObject && Equals(jobObject);
        }

        public override int GetHashCode()
        {
            return PathToFile != null ? PathToFile.GetHashCode() : 0;
        }

        private bool Equals(JobObject other)
        {
            return PathToFile == other.PathToFile;
        }
    }
}