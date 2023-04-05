using System.Text.Json;
using MessagingSystem.Application.Exceptions;
using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.DataAccess;

public class ReportRepository<T> : IRepository<Report>
{
    private string _path;
    private List<Report> _reports;

    public ReportRepository(string path, List<Report> reports)
    {
        _path = path;
        _reports = reports;
    }

    public void Add(Report entity)
    {
        _reports.Add(entity);
    }

    public void Delete(Report entity)
    {
        _reports.Remove(entity);
    }

    public Report GetById(Guid id)
    {
        return _reports.FirstOrDefault(x => x.Id == id)
               ?? throw new EmployeeException("Message not found");
    }

    public List<Report> GetAll()
    {
        return _reports;
    }

    public void Serialize()
    {
        string jsonString = JsonSerializer.Serialize(_reports);
        File.AppendAllText(_path, jsonString);
    }

    public void Deserialize()
    {
        string jsonString = File.ReadAllText(_path);
        _reports = JsonSerializer.Deserialize<List<Report>>(jsonString) !;
    }
}