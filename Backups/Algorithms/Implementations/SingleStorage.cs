﻿using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace Backups.Algorithms.Implementations
{
    public class SingleStorage : SavingAlgorithm
    {
        public SingleStorage(IFileSystem fileSystem)
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
                FileSystem.CopyFile(jobObject.PathToFile, SwapDirectory + "\\" + FileSystem.GetNameFromPath(jobObject.PathToFile));
            }

            FileSystem.AddToArchive(SwapDirectory, BackupsDirectory + $"\\{backupName}");
            foreach (JobObject jobObject in jobObjects)
            {
                FileSystem.RemoveFile(SwapDirectory + FileSystem.GetNameFromPath(jobObject.PathToFile));
            }

            FileSystem.RemoveDirectory(SwapDirectory);
        }

        public override void SetFileSystem(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }
    }
}