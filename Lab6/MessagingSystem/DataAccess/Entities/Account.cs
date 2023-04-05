using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.DataAccess;

public class Account
{
    public Account(string username, string password, Employee owner)
    {
        Username = username;
        Password = password;
        Owner = owner;
        Id = Guid.NewGuid();
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public Employee Owner { get; set; }
    public Guid Id { get; }
}