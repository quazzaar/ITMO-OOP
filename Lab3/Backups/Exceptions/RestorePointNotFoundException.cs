using Backups.Entities;

namespace Backups.Exceptions;

public class RestorePointNotFoundException : Exception
{
    public RestorePointNotFoundException(RestorePoint restorePoint)
        : base($"Restore point {restorePoint} not found")
    {
    }
}
