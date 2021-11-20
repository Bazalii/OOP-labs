﻿using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace Backups.Algorithms.Implementations
{
    public class SplitStorage : SavingAlgorithm
    {
        public SplitStorage(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
            BackupsDirectory = fileSystem.GetRoot() + "\\Backups";
            SwapDirectory = fileSystem.GetRoot() + "\\Swap";
            FileSystem.CreateDirectory(BackupsDirectory);
        }

        public override void Backup(List<JobObject> jobObjects, string backupName)
        {
            FileSystem.CreateDirectory(SwapDirectory);
            foreach (JobObject jobObject in jobObjects)
            {
                FileSystem.CopyFile(jobObject.PathToFile, SwapDirectory + "\\" + FileSystem.GetFullNameFromPath(jobObject.PathToFile));
                FileSystem.AddToArchive(SwapDirectory, BackupsDirectory + $"\\{backupName + FileSystem.GetNameFromPath(jobObject.PathToFile)}");
                FileSystem.RemoveFile(SwapDirectory + "\\" + FileSystem.GetFullNameFromPath(jobObject.PathToFile));
            }

            FileSystem.RemoveDirectory(SwapDirectory);
        }

        public override void SetFileSystem(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }
    }
}