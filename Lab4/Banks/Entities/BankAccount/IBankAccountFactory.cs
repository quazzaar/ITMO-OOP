namespace Banks.Entities;

public interface IBankAccountFactory
{
    IBankAccount CreateDebitBankAccount(Client client, BankAccountConditions bankAccountConditions);
    IBankAccount CreateDepositBankAccount(Client client, BankAccountConditions bankAccountConditions);
    IBankAccount CreateCreditBankAccount(Client client, BankAccountConditions bankAccountConditions);
}