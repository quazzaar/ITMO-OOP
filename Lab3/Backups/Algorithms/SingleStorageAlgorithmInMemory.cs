using System.IO.Compression;
using Backups.Entities;
using Zio.FileSystems;

namespace Backups.Algorithms;

public class SingleStorageAlgorithmInMemory : IAlgorithm
{
    public void CreateStorage(Backup backup, RestorePoint restorePoint)
    {
        var archiveInMemory = new ArchiveInMemory(restorePoint.BackupObjects.First().Path);
        foreach (var backupObject in restorePoint.BackupObjects)
        {
            restorePoint.CreateStorage(backupObject);
            var storage = restorePoint.Storages.ToList()[0];
            archiveInMemory.AddStorageToArchive(storage);
        }
    }
}