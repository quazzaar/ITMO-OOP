namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;
    private readonly List<Storage> _storages;
    public RestorePoint(List<BackupObject> backupObjects, DateTime timeOfCreation)
    {
        _backupObjects = backupObjects;
        _storages = new List<Storage>();
        TimeOfCreation = timeOfCreation;
        NumberOfRestorePoint = 1;
    }

    public DateTime TimeOfCreation { get; set; }
    public int NumberOfRestorePoint { get; set; }
    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public IReadOnlyCollection<Storage> Storages => _storages;

    public void CreateStorage(BackupObject backupObject)
    {
        _storages.Add(new Storage(backupObject));
    }
}