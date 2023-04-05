using Banks.Exceptions;

namespace Banks.Entities;

public class DebitAccount : IBankAccount
{
    private readonly decimal _suspiciousRestriction;
    private readonly decimal _percent;
    private decimal _balance;
    private decimal _cashback;

    public DebitAccount(Client client, BankAccountConditions bankAccountConditions)
    {
        Client = client;
        _suspiciousRestriction = bankAccountConditions.SuspiciousRestriction;
        _percent = bankAccountConditions.DebitPercent;
        _balance = 0;
    }

    public Client Client { get; set; }
    public void TopUp(decimal money)
    {
        _balance += money;
    }

    public void Withdraw(decimal money)
    {
        if (_balance < money)
        {
            throw new InvalidBankAccountException("There are not enough funds in your account to make a transfer");
        }

        if (Client.IsSuspicious && money > _suspiciousRestriction)
        {
            throw new InvalidTransactionException("Your account not verified, you can not withdraw these money until you enter your passport id and adress");
        }

        _balance -= money;
    }

    public void Transfer(IBankAccount recipient, decimal money)
    {
        Withdraw(money);
        recipient.TopUp(money);
    }

    public void MonthlyAccountBalanceChange(int time)
    {
        _cashback = time * (_balance * (_percent / 100)) / 365;
        _balance += _cashback;
        _cashback = 0;
    }

    public decimal GetBalance() => _balance;

    public string GetBankAccountConditions()
    {
        var debitPercent = _percent;
        var result = "Your bank account conditions: Debit percent - " + debitPercent;
        return result;
    }
}