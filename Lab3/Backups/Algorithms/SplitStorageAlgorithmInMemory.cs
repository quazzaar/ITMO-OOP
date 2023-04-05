using Backups.Entities;

namespace Backups.Algorithms;

public class SplitStorageAlgorithmInMemory : IAlgorithm
{
    public void CreateStorage(Backup backup, RestorePoint restorePoint)
    {
        foreach (var backupObject in restorePoint.BackupObjects)
        {
            restorePoint.CreateStorage(backupObject);
            var storage = restorePoint.Storages.ToList()[0];
            storage.BackupObject.Name = backupObject.Name + "(" + restorePoint.NumberOfRestorePoint + ").zip";
        }
    }
}