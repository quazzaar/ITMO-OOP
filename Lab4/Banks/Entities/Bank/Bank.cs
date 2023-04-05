using Banks.Exceptions;

namespace Banks.Entities;

public class Bank : IObservable
{
    private List<Client> _clients;
    private List<IBankAccount> _bankAccounts;
    private List<ICommand> _commands;
    private BankAccountConditions _bankAccountConditions;
    private ICommand _command = null!;
    private List<IObserver> _observers;

    public Bank(BankAccountConditions bankAccountConditions)
    {
        _clients = new List<Client>();
        _bankAccounts = new List<IBankAccount>();
        _commands = new List<ICommand>();
        _bankAccountConditions = bankAccountConditions;
        _observers = new List<IObserver>();
        ClientBuilder = new ClientBuilder();
        BankAccountFactory = new BankAccountFactory();
    }

    private ClientBuilder ClientBuilder { get; }
    private BankAccountFactory BankAccountFactory { get; }

    public Client AddClientToBank(string name, string surname, string address, string passport)
    {
        ClientBuilder.SetName(name);
        ClientBuilder.SetSurname(surname);
        ClientBuilder.SetAddress(address);
        ClientBuilder.SetPassport(passport);
        var client = ClientBuilder.Build();
        _clients.Add(client);
        return client;
    }

    public IBankAccount CreateDebitAccount(Client client)
    {
        var debitBankAccount = BankAccountFactory.CreateDebitBankAccount(client, _bankAccountConditions);
        _bankAccounts.Add(debitBankAccount);
        return debitBankAccount;
    }

    public IBankAccount CreateDepositAccount(Client client)
    {
        var depositBankAccount = BankAccountFactory.CreateDepositBankAccount(client, _bankAccountConditions);
        _bankAccounts.Add(depositBankAccount);
        return depositBankAccount;
    }

    public IBankAccount CreateCreditAccount(Client client)
    {
        var creditBankAccount = new CreditAccount(client, _bankAccountConditions);
        _bankAccounts.Add(creditBankAccount);
        return creditBankAccount;
    }

    public List<IBankAccount> GetAllBankAccounts() => _bankAccounts;

    public Client GetClientByPassport(string passport)
    {
        return _clients.Find(x => x.Passport == passport) ??
               throw new InvalidClientException("Client does not found");
    }

    public IBankAccount GetAccountByClient(Client client)
    {
        return _bankAccounts.Find(x => x.Client == client)
               ?? throw new InvalidBankAccountException("You dont have bank account");
    }

    public void SetCommand(ICommand command)
    {
        _command = command;
        _commands.Add(command);
    }

    public void RunCommand()
    {
        _command.Execute();
    }

    public void CancelCommand(ICommand command)
    {
        command.Undo();
    }

    public void ChangeDebitPercent(decimal debitPercent)
    {
        _bankAccountConditions.DebitPercent = debitPercent;
        Notify();
    }

    public void ChangeSuspiciousRestriction(decimal suspiciousRestriction)
    {
        _bankAccountConditions.SuspiciousRestriction = suspiciousRestriction;
        Notify();
    }

    public void AddSubscriber(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveSubscriber(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}