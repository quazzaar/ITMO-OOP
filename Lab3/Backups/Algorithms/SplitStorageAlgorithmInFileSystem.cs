using System.IO.Compression;
using Backups.Entities;
using Zio;
using Zio.FileSystems;

namespace Backups.Algorithms;

public class SplitStorageAlgorithmInFileSystem : IAlgorithm
{
    public void CreateStorage(Backup backup, RestorePoint restorePoint)
    {
        var fs = new PhysicalFileSystem();
        var pathToStorage = fs.ConvertPathFromInternal(restorePoint.BackupObjects.First().Path);

        foreach (var backupObject in restorePoint.BackupObjects)
        {
            var pathToDirectory = pathToStorage + "_" + backup.Name + "/" + backupObject.Name;
            fs.CreateDirectory(pathToDirectory);
            fs.CopyFile(backupObject.Path + "/" + backupObject.Name, pathToDirectory + "/" + backupObject.Name, true);
            ZipFile.CreateFromDirectory(pathToDirectory, pathToDirectory + "_" + restorePoint.TimeOfCreation + ".zip");
            fs.DeleteDirectory(pathToDirectory, true);
        }
    }
}