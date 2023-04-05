namespace Banks.Entities;

public class BankAccountFactory : IBankAccountFactory
{
    public IBankAccount CreateDebitBankAccount(Client client, BankAccountConditions bankAccountConditions)
    {
        return new DebitAccount(client, bankAccountConditions);
    }

    public IBankAccount CreateDepositBankAccount(Client client, BankAccountConditions bankAccountConditions)
    {
        return new DepositAccount(client, bankAccountConditions);
    }

    public IBankAccount CreateCreditBankAccount(Client client, BankAccountConditions bankAccountConditions)
    {
        return new CreditAccount(client, bankAccountConditions);
    }
}