using System.IO.Compression;
using Backups.Entities;
using Microsoft.VisualBasic;
using Zio;
using Zio.FileSystems;

namespace Backups.Algorithms;

public class SingleStorageAlgorithmInFileSystem : IAlgorithm
{
    public void CreateStorage(Backup backup, RestorePoint restorePoint)
    {
        var fs = new PhysicalFileSystem();
        var pathToStorage = fs.ConvertPathFromInternal(restorePoint.BackupObjects.First().Path);
        var sPath = fs.ConvertPathToInternal(pathToStorage);
        var pathToDirectory = pathToStorage + "_" + backup.Name;
        fs.CreateDirectory(pathToStorage + "_" + backup.Name);

        ZipFile.CreateFromDirectory(sPath, pathToDirectory + "/" + restorePoint.TimeOfCreation + ".zip");
    }
}