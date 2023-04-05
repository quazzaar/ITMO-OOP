using Banks.Exceptions;

namespace Banks.Entities;

public class CreditAccount : IBankAccount
{
    private readonly decimal _suspiciousRestriction;
    private readonly decimal _commission;
    private readonly decimal _creditLimit;
    private decimal _balance;

    public CreditAccount(Client client, BankAccountConditions bankAccountConditions)
    {
        Client = client;
        _suspiciousRestriction = bankAccountConditions.SuspiciousRestriction;
        _commission = bankAccountConditions.CreditCommission;
        _creditLimit = bankAccountConditions.CreditLimit;
        _balance = 0;
    }

    public Client Client { get; set; }
    public void TopUp(decimal money)
    {
        _balance += money;
    }

    public void Withdraw(decimal money)
    {
        if (Math.Abs(_creditLimit) < Math.Abs(_balance - money))
        {
            throw new InvalidBankAccountException("You reach credit limit");
        }

        if (Client.IsSuspicious && money > _suspiciousRestriction)
        {
            throw new InvalidTransactionException(
                "Your account not verified, you can not withdraw these money until you enter your passport id and adress");
        }

        if (_balance > money)
        {
            _balance -= money + _commission;
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
        if (_balance < 0 && time / 31 > 1)
        {
            _balance -= _commission * (time / 31);
        }
    }

    public decimal GetBalance() => _balance;

    public string GetBankAccountConditions()
    {
        var creditCommission = _commission.ToString();
        var creditLimit = _creditLimit.ToString();
        var result = "Your bank account conditions: Credit Limit - " + creditLimit + ", Credit commission - " +
                      creditCommission;
        return result;
    }
}