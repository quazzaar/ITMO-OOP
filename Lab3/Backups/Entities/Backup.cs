using Backups.Exceptions;
using Backups.Repositories;

namespace Backups.Entities;

public class Backup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup(string name)
    {
        Name = name;
        _restorePoints = new List<RestorePoint>();
    }

    public string Name { get; set; }
    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _restorePoints.Add(restorePoint);
        restorePoint.NumberOfRestorePoint = _restorePoints.Count;
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        if (!_restorePoints.Contains(restorePoint))
            throw new RestorePointNotFoundException(restorePoint);
        _restorePoints.Remove(restorePoint);
    }
}