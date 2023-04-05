namespace Backups.Entities;

public class ArchiveInMemory
{
    private List<Storage> _storages;

    public ArchiveInMemory(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentNullException();
        Path = path + ".zip";
        _storages = new List<Storage>();
    }

    public string Path { get; }

    public void AddStorageToArchive(Storage storage)
    {
        _storages.Add(storage);
    }
}