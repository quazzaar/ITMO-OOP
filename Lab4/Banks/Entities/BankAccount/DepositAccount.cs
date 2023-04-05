using Banks.Exceptions;

namespace Banks.Entities;

public class DepositAccount : IBankAccount
{
    private readonly decimal _suspiciousRestriction;
    private decimal _percent;
    private int _expirationDate;
    private decimal _balance;
    private decimal _lowInterest;
    private decimal _middleInterest;
    private decimal _highInterest;
    private decimal _depositRateIncreasing;

    public DepositAccount(Client client, BankAccountConditions bankAccountConditions)
    {
        Client = client;
        _suspiciousRestriction = bankAccountConditions.SuspiciousRestriction;
        _percent = bankAccountConditions.DepositPercent;
        _depositRateIncreasing = bankAccountConditions.DepositRateIncreasing;
        _expirationDate = bankAccountConditions.ExpirationDate;
        _balance = 0;
    }

    public Client Client { get; set; }
    public void TopUp(decimal money)
    {
        _balance += money;
    }

    public void Withdraw(decimal money)
    {
        if (_expirationDate > 0)
        {
            throw new InvalidBankAccountException(
                "You can not withdraw your money until your bank account does not expired");
        }

        if (_balance < money)
        {
            throw new InvalidBankAccountException(
                "There are not enough funds in your account to make a transfer");
        }

        if (Client.IsSuspicious && money > _suspiciousRestriction)
        {
            throw new InvalidTransactionException(
                "Your account not verified, you can not withdraw these money until you enter your passport id and adress");
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
        _expirationDate -= time;
        if (_expirationDate < 0)
        {
            if (_balance >= 100000)
            {
                _highInterest = (_percent + (2 * _depositRateIncreasing)) / 100;
                _balance += _balance * _highInterest * time;
            }
            else if (_balance <= 100000 && _balance >= 50000)
            {
                _middleInterest = (_percent + _depositRateIncreasing) / 100;
                _balance += _balance * _middleInterest * time;
            }
            else
            {
                _lowInterest = _percent / 100;
                _balance += _balance * _lowInterest * time;
            }
        }
        else
        {
            throw new InvalidBankAccountException(
                "You can not withdraw your money until your bank account does not expired");
        }
    }

    public decimal GetBalance() => _balance;

    public string GetBankAccountConditions()
    {
        var depositPercent = _percent.ToString();
        var rate = _depositRateIncreasing.ToString();
        var expirationDate = _expirationDate.ToString();
        var result = "Your bank account conditions: Deposit percent - " + depositPercent + ", Rate - " +
                     rate + ", Expiration date - " + expirationDate;
        return result;
    }
}