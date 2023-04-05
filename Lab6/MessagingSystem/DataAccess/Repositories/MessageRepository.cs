using System.Text.Json;
using MessagingSystem.Application.Exceptions;
using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.DataAccess;

public class MessageRepository<T> : IRepository<Message>
{
    private string _path;
    private List<Message> _messages;

    public MessageRepository(string path, List<Message> messages)
    {
        _path = path;
        _messages = messages;
    }

    public void Add(Message entity)
    {
        _messages.Add(entity);
    }

    public void Delete(Message entity)
    {
        _messages.Remove(entity);
    }

    public Message GetById(Guid id)
    {
        return _messages.FirstOrDefault(x => x.Id == id)
               ?? throw new EmployeeException("Message not found");
    }

    public List<Message> GetAll()
    {
        return _messages;
    }

    public void Serialize()
    {
        string jsonString = JsonSerializer.Serialize(_messages);
        File.AppendAllText(_path, jsonString);
    }

    public void Deserialize()
    {
        string jsonString = File.ReadAllText(_path);
        _messages = JsonSerializer.Deserialize<List<Message>>(jsonString) !;
    }
}