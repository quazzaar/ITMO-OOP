using Backups.Algorithms;
using Backups.Exceptions;

namespace Backups.Entities;

public class Storage
{
    public Storage(BackupObject backupObject)
    {
        if (backupObject == null)
            throw new ArgumentNullException();
        BackupObject = backupObject;
    }

    public BackupObject BackupObject { get; set; }
}