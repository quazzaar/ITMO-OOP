namespace Banks.Entities;

public class CentralBank
{
    private static CentralBank _instance = null!;
    private List<Bank> _banks;

    private CentralBank()
    {
        _banks = new List<Bank>();
    }

    public static CentralBank GetCentralBankInstance()
    {
        if (_instance == null)
        {
            _instance = new CentralBank();
        }

        return _instance;
    }

    public Bank RegisterBank(
        string name,
        decimal suspiciousRestriction,
        decimal debitPercent,
        decimal depositPercent,
        decimal depositRateIncreasing,
        decimal creditLimit,
        decimal creditCommission,
        int expirationDate)
    {
        BankAccountConditions bankAccountConditions = new BankAccountConditions(
            name,
            suspiciousRestriction,
            debitPercent,
            depositPercent,
            depositRateIncreasing,
            creditLimit,
            creditCommission,
            expirationDate);

        var bank = new Bank(bankAccountConditions);
        _banks.Add(bank);
        return bank;
    }

    public void TimeRemote(int daysToSkip)
    {
        foreach (var bank in _banks)
        {
            var bankAccounts = bank.GetAllBankAccounts();
            foreach (var bankAccount in bankAccounts)
            {
                bankAccount.MonthlyAccountBalanceChange(daysToSkip);
            }
        }
    }

    public void Transfer(Bank bank, IBankAccount accountFrom, IBankAccount accountTo, decimal money)
    {
        bank.SetCommand(new TransferCommand(accountFrom, accountTo, money));
        bank.RunCommand();
    }

    public void TopUp(Bank bank, IBankAccount account, decimal money)
    {
        bank.SetCommand(new TopUpCommand(account, money));
        bank.RunCommand();
    }

    public void Withdraw(Bank bank, IBankAccount account, decimal money)
    {
        bank.SetCommand(new WithdrawCommand(account, money));
        bank.RunCommand();
    }

    public void CancelOperation(Bank bank, ICommand command)
    {
        bank.CancelCommand(command);
    }
}