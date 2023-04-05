using System.Net.Http.Json;
using System.Text.Json;
using MessagingSystem.Application.Exceptions;
using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.DataAccess;

public class AccountRepository<T> : IRepository<Account>
{
    private string _path;
    private List<Account> _accounts;
    public AccountRepository(string path, List<Account> accounts)
    {
        _path = path;
        _accounts = accounts;
    }

    public void Add(Account entity)
    {
        _accounts.Add(entity);
    }

    public void Delete(Account entity)
    {
        _accounts.Remove(entity);
    }

    public Account GetById(Guid id)
    {
        return _accounts.FirstOrDefault(x => x.Id == id)
               ?? throw new AccountException("Account not found");
    }

    public List<Account> GetAll()
    {
        return _accounts;
    }

    public void Serialize()
    {
        string jsonString = JsonSerializer.Serialize(_accounts);
        File.AppendAllText(_path, jsonString);
    }

    public void Deserialize()
    {
        string jsonString = File.ReadAllText(_path);
        _accounts = JsonSerializer.Deserialize<List<Account>>(jsonString) !;
    }
}