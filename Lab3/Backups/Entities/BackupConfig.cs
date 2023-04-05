using Backups.Algorithms;
using Backups.Repositories;

namespace Backups.Entities;

public class BackupConfig
{
    public BackupConfig(IAlgorithm algorithm, IRepository repository)
    {
        Algorithm = algorithm;
        Repository = repository;
    }

    public IAlgorithm Algorithm { get; private set; }
    public IRepository Repository { get; private set; }

    public void SetAlgorithm(IAlgorithm algorithm)
    {
        Algorithm = algorithm;
    }

    public void SetRepository(IRepository repository)
    {
        Repository = repository;
    }
}