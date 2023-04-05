using MessagingSystem.Application.Exceptions;
using MessagingSystem.DataAccess;
using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.Application;

public class MessageSystemDecorator : IMessageSystemService
{
    private IMessageSystemService _inner;

    public MessageSystemDecorator(IMessageSystemService inner)
    {
        _inner = inner;
    }

    public Employee CreateChief(string name, string surname)
    {
        var employee = _inner.CreateChief(name, surname);

        return employee;
    }

    public Account CreateAccount(string username, string password, Employee owner)
    {
        if (owner.Position != Employee.EmployeePosition.Chief)
        {
            throw new EmployeeException("You can't create employee");
        }

        var account = _inner.CreateAccount(username, password, owner);
        return account;
    }

    public void RecieveMessage(Employee employee, Message message)
    {
        if (employee.Position != Employee.EmployeePosition.Subordinate)
        {
            throw new EmployeeException("You can't receive message");
        }

        _inner.RecieveMessage(employee, message);
    }

    public void ProcessMessage(Employee employee, Message message)
    {
        if (employee.Position != Employee.EmployeePosition.Subordinate)
        {
            throw new EmployeeException("You can't process message");
        }

        _inner.ProcessMessage(employee, message);
    }

    public void CreateReport(Employee employee)
    {
        if (employee.Position != Employee.EmployeePosition.Manager)
        {
            throw new EmployeeException("You can't process message");
        }

        _inner.CreateReport(employee);
    }

    public Employee CreateEmployee(string name, string surname, Employee.EmployeePosition position)
    {
        if (position != Employee.EmployeePosition.Chief)
        {
            throw new EmployeeException("You can't create employee");
        }

        var employee = _inner.CreateEmployee(name, surname, position);
        return employee;
    }

    public void ChangeNameOfEmployee(string newName, Employee employee)
    {
        if (employee.Position == Employee.EmployeePosition.Chief)
        {
            _inner.ChangeNameOfEmployee(newName, employee);
        }
        else
        {
            throw new EmployeeException("You can't change name of employee");
        }
    }

    public void ChangeSurnameOfEmployee(string newSurname, Employee employee)
    {
        if (employee.Position == Employee.EmployeePosition.Chief)
        {
            _inner.ChangeSurnameOfEmployee(newSurname, employee);
        }
        else
        {
            throw new EmployeeException("You can't change surname of employee");
        }
    }

    public void ChangePositionOfEmployee(Employee.EmployeePosition newPosition, Employee employee)
    {
        if (employee.Position == Employee.EmployeePosition.Chief)
        {
            _inner.ChangePositionOfEmployee(newPosition, employee);
        }
        else
        {
            throw new EmployeeException("You can't change position of employee");
        }
    }

    public Employee GetEmployeeById(Guid id)
    {
        var employee = _inner.GetEmployeeById(id);

        return employee;
    }

    public void Start(string path, List<Account> accounts, List<Employee> employees, List<Message> messages, List<Report> reports)
    {
        var messageSystem = new MessageSystemService(path, employees, messages, accounts, reports);
        var employeeRepository = new EmployeeRepository<Employee>(path, employees);
        var accountRepository = new AccountRepository<Account>(path, accounts);
        var messageRepository = new MessageRepository<Message>(path, messages);
        var reportRepository = new ReportRepository<Message>(path, reports);
        Console.WriteLine("click '1' to create chief");
        Console.WriteLine("click '2' to create or edit employee");
        Console.WriteLine("click '3' to create account");
        Console.WriteLine("click '4' to create message");
        Console.WriteLine("click '5' to work with message");
        Console.WriteLine("click '6' to create report");
        Console.WriteLine("click '7' to get all data");
        Console.WriteLine("enter 'exit' to stop program");
        while (true)
        {
            var a = Console.ReadLine();
            switch (a)
            {
                case "1":
                    Console.WriteLine("enter name of chief");
                    var name = Console.ReadLine();
                    Console.WriteLine("enter surname of chief");
                    var surname = Console.ReadLine();
                    var chief = messageSystem.CreateChief(name!, surname!);
                    Console.WriteLine($"chief with id {chief.Id} was created");
                    break;
                case "2":
                    Console.WriteLine("enter name of employee");
                    var nameOfEmployee = Console.ReadLine();
                    Console.WriteLine("enter surname of employee");
                    var surnameOfEmployee = Console.ReadLine();
                    Console.WriteLine("select position of employee");
                    Console.WriteLine("1 - manager");
                    Console.WriteLine("2 - subordinate");
                    var position = Console.ReadLine();
                    Employee.EmployeePosition positionOfEmployee;
                    switch (position)
                    {
                        case "1":
                            positionOfEmployee = Employee.EmployeePosition.Manager;
                            break;
                        case "2":
                            positionOfEmployee = Employee.EmployeePosition.Subordinate;
                            break;
                        default:
                            throw new EmployeeException("Position doesn't exist");
                    }

                    var employee = messageSystem.CreateEmployee(nameOfEmployee!, surnameOfEmployee!, positionOfEmployee);
                    GetEmployeeById(employee.Id);
                    Console.WriteLine($"employee with id {employee.Id} was created");
                    break;
                case "3":
                    Console.WriteLine("enter username of account");
                    var username = Console.ReadLine();
                    Console.WriteLine("enter password of account");
                    var password = Console.ReadLine();
                    Console.WriteLine("enter id of employee");
                    var id = Guid.Parse(Console.ReadLine() !);
                    var employee1 = messageSystem.GetEmployeeById(id);
                    var account = messageSystem.CreateAccount(username!, password!, employee1);
                    Console.WriteLine($"account with id {account.Id} was created");
                    break;
                case "4":
                    Console.WriteLine("enter id of recipient");
                    var idOfRecipient = Guid.Parse(Console.ReadLine() !);
                    var recipient = messageSystem.GetEmployeeById(idOfRecipient);
                    Console.WriteLine("enter text of message");
                    var textOfMessage = Console.ReadLine();
                    Console.WriteLine("select source of message");
                    Console.WriteLine("1 - email");
                    Console.WriteLine("2 - sms");
                    Console.WriteLine("3 - messenger");
                    var str = Console.ReadLine();
                    MessageSource.SourceOfMessage sourceOfMessage;
                    switch (str)
                    {
                        case "1":
                            sourceOfMessage = MessageSource.SourceOfMessage.Email;
                            break;
                        case "2":
                            sourceOfMessage = MessageSource.SourceOfMessage.Email;
                            break;
                        case "3":
                            sourceOfMessage = MessageSource.SourceOfMessage.Email;
                            break;
                        default:
                            throw new EmployeeException("Source doesn't exist");
                    }

                    var message = messageSystem.CreateMessage(textOfMessage!, sourceOfMessage, recipient);
                    Console.WriteLine($"message with id {message.Id} was created");
                    break;

                case "5":
                    Console.WriteLine("enter id of employee");
                    var idOfEmployee = Guid.Parse(Console.ReadLine() !);
                    var employee2 = messageSystem.GetEmployeeById(idOfEmployee);
                    Console.WriteLine("enter id of message");
                    var idOfMessage = Guid.Parse(Console.ReadLine() !);
                    var message2 = messageSystem.GetMessageById(idOfMessage);
                    Console.WriteLine("enter '1' to receive message");
                    Console.WriteLine("enter '2' to process message");
                    var b = Console.ReadLine();
                    switch (b)
                    {
                        case "1":
                            messageSystem.RecieveMessage(employee2, message2);
                            Console.WriteLine("message was received");
                            break;
                        case "2":
                            messageSystem.ProcessMessage(employee2, message2);
                            Console.WriteLine("message was processed");
                            break;
                    }

                    break;
                case "6":
                    Console.WriteLine("enter id of employee");
                    var idOfEmployee1 = Guid.Parse(Console.ReadLine() !);
                    var employee3 = messageSystem.GetEmployeeById(idOfEmployee1);
                    messageSystem.CreateReport(employee3);
                    Console.WriteLine("report was created");
                    break;
                case "7":
                    Console.WriteLine("enter '1' to get all employees");
                    Console.WriteLine("enter '2' to get all accounts");
                    Console.WriteLine("enter '3' to get all messages");
                    Console.WriteLine("enter '4' to get all reports");
                    var c = Console.ReadLine();
                    switch (c)
                    {
                        case "1":
                            messageSystem.GetAllEmployees();
                            foreach (var employee4 in employees)
                            {
                                Console.WriteLine($"id: {employee4.Id}, name: {employee4.Name}, surname: {employee4.Surname}, position: {employee4.Position}");
                            }

                            break;
                        case "2":
                            messageSystem.GetAllAccounts();
                            foreach (var account1 in accounts)
                            {
                                Console.WriteLine($"id: {account1.Id}, username: {account1.Username}, password: {account1.Password}, employee id: {account1.Owner.Id}");
                            }

                            break;
                        case "3":
                            messageSystem.GetAllMessages();
                            foreach (var message1 in messages)
                            {
                                Console.WriteLine($"id: {message1.Id}, text: {message1.MessageText}, status: {message1.State}, employee id: {message1.Recipient.Id}");
                            }

                            break;
                        case "4":
                            messageSystem.GetAllReports();
                            foreach (var report in reports)
                            {
                                Console.WriteLine($"id: {report.Id}, text: {report.ReportText}, employee id: {report.ReportDate}");
                            }

                            break;
                    }

                    break;
            }
        }
    }
}