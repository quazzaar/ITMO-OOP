using MessagingSystem.Application;
using MessagingSystem.DataAccess;
using MessagingSystem.DataAccess.Entities;
using Xunit;

namespace MessagingSystem.Tests;

public class MessagingSystemTests
{
    [Fact]
    public void CheckCreatingChief()
    {
        var employees = new List<Employee>();
        var accounts = new List<Account>();
        var messages = new List<Message>();
        var reports = new List<Report>();
        var messageSystem =
            new MessageSystemService("123", employees, messages, accounts, reports);
        var newEmployee = messageSystem.CreateChief("Dimas", "Ananas");
        Assert.True(newEmployee.Position == Employee.EmployeePosition.Chief);
    }

    [Fact]
    public void CheckCreatingMessage()
    {
        var employees = new List<Employee>();
        var accounts = new List<Account>();
        var messages = new List<Message>();
        var reports = new List<Report>();
        var messageSystem =
            new MessageSystemService("123", employees, messages, accounts, reports);
        var newEmployee = messageSystem.CreateEmployee("Dimas", "Ananas", Employee.EmployeePosition.Chief);
        var messageText = "Hello, world!";
        MessageSource.SourceOfMessage messageSource = MessageSource.SourceOfMessage.Messenger;
        var newMessage = messageSystem.CreateMessage(messageText, messageSource, newEmployee);
        Assert.True(newMessage.MessageText == "Hello, world!");

    }

    [Fact]
    public void CheckCreatingAccount()
    {
        var employees = new List<Employee>();
        var accounts = new List<Account>();
        var messages = new List<Message>();
        var reports = new List<Report>();
        var messageSystem =
            new MessageSystemService("123", employees, messages, accounts, reports);
        var newEmployee = messageSystem.CreateEmployee("Dimas", "Ananas", Employee.EmployeePosition.Chief);
        var newAccount = messageSystem.CreateAccount("qwerty", "123456", newEmployee);
        Assert.True(newAccount.Owner.Name == "Dimas");
    }

    [Fact]
    public void CheckAuthorization()
    {
        var employees = new List<Employee>();
        var accounts = new List<Account>();
        var messages = new List<Message>();
        var reports = new List<Report>();
        var messageSystem =
            new MessageSystemService("/Users/dmitrijnadezdin/Documents/GitHub/quazzaar/MessagingSystem/DataBase/data.json", employees, messages, accounts, reports);
        var newEmployee = messageSystem.CreateEmployee("Dimas", "Ananas", Employee.EmployeePosition.Chief);
        var newAccount = messageSystem.CreateAccount("qwerty", "123456", newEmployee);
        messageSystem.Authorize("qwerty", "123", newEmployee);
        Assert.True(newEmployee.Position == Employee.EmployeePosition.Anonymous);
    }

    [Fact]
    public void EmployeeCanChangeStateOfMessage()
    {
        var employees = new List<Employee>();
        var accounts = new List<Account>();
        var messages = new List<Message>();
        var reports = new List<Report>();
        var messageSystem =
            new MessageSystemService("/Users/dmitrijnadezdin/Documents/GitHub/quazzaar/MessagingSystem/DataBase/data.json", employees, messages, accounts, reports);
        var newEmployee = messageSystem.CreateEmployee("Dimas", "Ananas", Employee.EmployeePosition.Chief);
        var messageText = "Hello, world!";
        MessageSource.SourceOfMessage messageSource = MessageSource.SourceOfMessage.Messenger;
        var newMessage = messageSystem.CreateMessage(messageText, messageSource, newEmployee);
        messageSystem.RecieveMessage(newEmployee, newMessage);
        Assert.True(newMessage.State == Message.MessageState.Received);
        messageSystem.ProcessMessage(newEmployee, newMessage);
        Assert.True(newMessage.State == Message.MessageState.Processed);
        Assert.True(newMessage.MessageText == "Processed");
    }
}

