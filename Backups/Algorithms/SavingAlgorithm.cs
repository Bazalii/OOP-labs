﻿using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace Backups.Algorithms
{
    public abstract class SavingAlgorithm
    {
        protected IFileSystem FileSystem { get; set; }

        protected string BackupsDirectory { get; set; }

        protected string SwapDirectory { get; set; }

        public abstract void Backup(List<JobObject> jobObjects, string backupName);

        public abstract void SetFileSystem(IFileSystem fileSystem);
    }
}