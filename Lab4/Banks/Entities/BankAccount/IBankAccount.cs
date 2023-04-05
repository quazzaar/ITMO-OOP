namespace Banks.Entities;

public interface IBankAccount
{
    Client Client { get; set; }
    void TopUp(decimal money);
    void Withdraw(decimal money);
    void Transfer(IBankAccount recipient, decimal money);
    void MonthlyAccountBalanceChange(int time);
    decimal GetBalance();
    string GetBankAccountConditions();
}