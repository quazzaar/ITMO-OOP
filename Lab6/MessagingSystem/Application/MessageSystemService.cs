using System.Text.Json;
using MessagingSystem.Application.Exceptions;
using MessagingSystem.DataAccess;
using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.Application;

public class MessageSystemService : IMessageSystemService
{
    private string _path;
    private Employee _instance = null!;
    private List<Employee> _employees;
    private List<Message> _messages;
    private List<Account> _accounts;
    private List<Report> _reports;

    public MessageSystemService(string path, List<Employee> employees, List<Message> messages, List<Account> accounts, List<Report> reports)
    {
        _path = path;
        _employees = employees;
        _messages = messages;
        _accounts = accounts;
        _reports = reports;
    }

    public Employee CreateChief(string name, string surname)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
        {
            throw new ArgumentException("Name and surname cannot be empty");
        }

        Employee.EmployeePosition position = Employee.EmployeePosition.Chief;

        if (_instance == null)
        {
            _instance = new Employee(name, surname, position);
        }
        else
        {
            throw new EmployeeException("Chief already exists");
        }

        AddEmployee(_instance);
        return _instance;
    }

    public Employee CreateEmployee(string name, string surname, Employee.EmployeePosition position)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
        {
            throw new ArgumentException("Name and surname cannot be empty");
        }

        var employeeBuilder = new EmployeeBuilder();
        Employee employee = employeeBuilder.Build(name, surname, position);
        AddEmployee(employee);
        return employee;
    }

    public void ChangeNameOfEmployee(string newName, Employee employee)
    {
        var employeeBuilder = new EmployeeBuilder();
        employeeBuilder.SetName(newName, employee);
    }

    public void ChangeSurnameOfEmployee(string newSurname, Employee employee)
    {
        var employeeBuilder = new EmployeeBuilder();
        employeeBuilder.SetSurname(newSurname, employee);
    }

    public void ChangePositionOfEmployee(Employee.EmployeePosition newPosition, Employee employee)
    {
        var employeeBuilder = new EmployeeBuilder();
        employeeBuilder.SetPosition(newPosition, employee);
    }

    public Account CreateAccount(string username, string password, Employee owner)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || owner == null)
        {
            throw new AccountException("Username or password cannot be empty");
        }

        var account = new Account(username, password, owner);
        AddAccount(account);

        return account;
    }

    public void Authorize(string username, string password, Employee employee)
    {
        var accountRepository = new AccountRepository<Account>(_path, _accounts);
        var accounts = accountRepository.GetAll();

        if (accounts.Any(a => a.Username == username) && accounts.Any(a => a.Password == password))
        {
            return;
        }

        ChangePositionOfEmployee(Employee.EmployeePosition.Anonymous, employee);
    }

    public Message CreateMessage(string messageText, MessageSource.SourceOfMessage source, Employee recipient)
    {
        if (string.IsNullOrEmpty(messageText) || recipient == null)
        {
            throw new MessageException("Message cannot be empty");
        }

        var state = Message.MessageState.New;
        var message = new Message(messageText, source, recipient, state);
        AddMessage(message);

        return message;
    }

    public void ChangeMessageState(Message.MessageState newState, Message message)
    {
        message.State = newState;
    }

    public void RecieveMessage(Employee employee, Message message)
    {
        if (message.Recipient != employee)
        {
            throw new MessageException("You cannot receive this message");
        }

        ChangeMessageState(Message.MessageState.Received, message);
    }

    public void ProcessMessage(Employee employee, Message message)
    {
        if (message.Recipient != employee)
        {
            throw new MessageException("You cannot receive this message");
        }

        ChangeMessageState(Message.MessageState.Processed, message);
        message.MessageText = "Processed";
    }

    public void CreateReport(Employee employee)
    {
        var messageRepository = new MessageRepository<Message>(_path, _messages);
        var messages = messageRepository.GetAll();
        foreach (var message in messages)
        {
            if (message.State == Message.MessageState.Processed)
            {
                    string reportText = "Report for " + message.Recipient.Name + " " + message.Recipient.Surname + " ";
                    var report = new Report(reportText);
                    AddReport(report);
            }
        }
    }

    public void SaveAllData()
    {
        var employeeRepository = new EmployeeRepository<Employee>(_path, _employees);
        employeeRepository.Serialize();
        var accountRepository = new AccountRepository<Account>(_path, _accounts);
        accountRepository.Serialize();
        var messageRepository = new MessageRepository<Message>(_path, _messages);
        messageRepository.Serialize();
        var reportRepository = new ReportRepository<Report>(_path, _reports);
        reportRepository.Serialize();
    }

    public void RecoverAllData()
    {
        var employeeRepository = new EmployeeRepository<Employee>(_path, _employees);
        employeeRepository.Deserialize();
        var accountRepository = new AccountRepository<Account>(_path, _accounts);
        accountRepository.Deserialize();
        var messageRepository = new MessageRepository<Message>(_path, _messages);
        messageRepository.Deserialize();
        var reportRepository = new ReportRepository<Report>(_path, _reports);
        reportRepository.Deserialize();
    }

    public Account GetAccountById(Guid id)
    {
        var accountRepository = new AccountRepository<Account>(_path, _accounts);
        var account = accountRepository.GetById(id);
        return account;
    }

    public Employee GetEmployeeById(Guid id)
    {
        var employeeRepository = new EmployeeRepository<Account>(_path, _employees);
        var employee = employeeRepository.GetById(id);
        return employee;
    }

    public Message GetMessageById(Guid id)
    {
        var messageRepository = new MessageRepository<Message>(_path, _messages);
        var message = messageRepository.GetById(id);
        return message;
    }

    public Report GetReportById(Guid id)
    {
        var reportRepository = new ReportRepository<Report>(_path, _reports);
        var report = reportRepository.GetById(id);
        return report;
    }

    public void AddAccount(Account account)
    {
        var accountRepository = new AccountRepository<Account>(_path, _accounts);
        accountRepository.Add(account);
    }

    public void AddEmployee(Employee employee)
    {
        var employeeRepository = new EmployeeRepository<Employee>(_path, _employees);
        employeeRepository.Add(employee);
    }

    public void AddMessage(Message message)
    {
        var messageRepository = new MessageRepository<Message>(_path, _messages);
        messageRepository.Add(message);
    }

    public void AddReport(Report report)
    {
        var reportRepository = new ReportRepository<Report>(_path, _reports);
        reportRepository.Add(report);
    }

    public void GetAllAccounts()
    {
        var accountRepository = new AccountRepository<Account>(_path, _accounts);
        var accounts = accountRepository.GetAll();
    }

    public void GetAllEmployees()
    {
        var employeeRepository = new EmployeeRepository<Employee>(_path, _employees);
        var employees = employeeRepository.GetAll();
    }

    public void GetAllMessages()
    {
        var messageRepository = new MessageRepository<Message>(_path, _messages);
        var messages = messageRepository.GetAll();
    }

    public void GetAllReports()
    {
        var reportRepository = new ReportRepository<Report>(_path, _reports);
        var reports = reportRepository.GetAll();
    }
}
