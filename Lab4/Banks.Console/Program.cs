using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities;

namespace Banks
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var centralBank = CentralBank.GetCentralBankInstance();
            var tinkoff = centralBank.RegisterBank("Tinkoff", 10000, 5, 3, 0.5m, 60000, 10000, 100);
            var sber = centralBank.RegisterBank("Sber", 20000, 6, 2, 0.5m, 50000, 15000, 60);
            Console.WriteLine("click '1' to create client");
            Console.WriteLine("click '2' to create bank account");
            Console.WriteLine("click '3' to make operation with your account");
            Console.WriteLine("click '4' to edit percents or commission");
            Console.WriteLine("enter 'exit' to stop program");
            while (true)
            {
                var a = Console.ReadLine();
                string passport;
                switch (a)
                {
                    case "exit":
                        return;
                    case "1":
                        Console.WriteLine("Lets make client account");
                        Console.WriteLine("you must enter name and surname");
                        Console.WriteLine("Enter your name");
                        string name = Console.ReadLine() !;
                        Console.WriteLine("Enter your surname");
                        string surname = Console.ReadLine() !;
                        Console.WriteLine("Enter your address");
                        string address = Console.ReadLine() !;
                        Console.WriteLine("Enter your passport id (6 numbers)");
                        passport = Console.ReadLine() !;
                        tinkoff.AddClientToBank(name, surname, address, passport);
                        break;
                    case "2":
                        Console.WriteLine("click '1' to create debit account");
                        Console.WriteLine("click '2' to create deposit account");
                        Console.WriteLine("click '3' to create credit account");
                        Console.WriteLine("enter 'exit' to stop program");
                        Client client;
                        var b = Console.ReadLine();
                        switch (b)
                        {
                            case "1":
                                Console.WriteLine("Enter your passport id (6 numbers)");
                                passport = Console.ReadLine() !;
                                client = tinkoff.GetClientByPassport(passport);
                                IBankAccount debitAccount = tinkoff.CreateDebitAccount(client);
                                Console.WriteLine(debitAccount.GetBankAccountConditions());
                                break;
                            case "2":
                                Console.WriteLine("Enter your passport id (6 numbers)");
                                passport = Console.ReadLine() !;
                                client = tinkoff.GetClientByPassport(passport);
                                IBankAccount depositAccount = tinkoff.CreateDepositAccount(client);
                                Console.WriteLine(depositAccount.GetBankAccountConditions());
                                break;
                            case "3":
                                Console.WriteLine("Enter your passport id (6 numbers)");
                                passport = Console.ReadLine() !;
                                client = tinkoff.GetClientByPassport(passport);
                                IBankAccount creditAccount = tinkoff.CreateCreditAccount(client);
                                Console.WriteLine(creditAccount.GetBankAccountConditions());
                                break;
                        }

                        break;

                    case "3":
                        Console.WriteLine("click '1' to top up your account");
                        Console.WriteLine("click '2' to withdraw from your account");
                        Console.WriteLine("click '3' to transfer");
                        Console.WriteLine("click '4' to subscribe on changes");
                        Console.WriteLine("click '5' to check your balance after skipping time");
                        var c = Console.ReadLine();
                        IBankAccount? account;
                        decimal money;
                        switch (c)
                        {
                            case "1":
                                Console.WriteLine("Enter your passport id (6 numbers)");
                                passport = Console.ReadLine() !;
                                client = tinkoff.GetClientByPassport(passport);
                                account = tinkoff.GetAccountByClient(client);
                                Console.WriteLine("Enter amount of your top up");
                                money = Convert.ToDecimal(Console.ReadLine());
                                var topUpCommand = new TopUpCommand(account, money);
                                tinkoff.SetCommand(topUpCommand);
                                tinkoff.RunCommand();
                                Console.WriteLine("Now your balance: " + account.GetBalance());
                                Console.WriteLine("Enter '1' if you want to undo this transaction");
                                Console.WriteLine("Enter '2' if you want to continue");
                                var d = Console.ReadLine();
                                switch (d)
                                {
                                    case "1":
                                        tinkoff.CancelCommand(topUpCommand);
                                        Console.WriteLine("Now your balance: " + account.GetBalance());
                                        break;
                                    case "2":
                                        break;
                                }

                                break;
                            case "2":
                                Console.WriteLine("Enter your passport id (6 numbers)");
                                passport = Console.ReadLine() !;
                                client = tinkoff.GetClientByPassport(passport);
                                account = tinkoff.GetAccountByClient(client);
                                Console.WriteLine("Enter amount of your withdraw");
                                money = Convert.ToDecimal(Console.ReadLine());
                                var withdrawCommand = new WithdrawCommand(account, money);
                                tinkoff.SetCommand(withdrawCommand);
                                tinkoff.RunCommand();
                                Console.WriteLine("Now your balance: " + account.GetBalance());
                                Console.WriteLine(account.GetBankAccountConditions());
                                Console.WriteLine("Enter '1' if you want to undo this transaction");
                                Console.WriteLine("Enter '2' if you want to continue");
                                var e = Console.ReadLine();
                                switch (e)
                                {
                                    case "1":
                                        tinkoff.CancelCommand(withdrawCommand);
                                        Console.WriteLine("Now your balance: " + account.GetBalance());
                                        break;
                                    case "2":
                                        break;
                                }

                                break;
                            case "3":
                                Console.WriteLine("Enter your passport id (6 numbers)");
                                passport = Console.ReadLine() !;
                                client = tinkoff.GetClientByPassport(passport);
                                account = tinkoff.GetAccountByClient(client);
                                Console.WriteLine("Enter passport id to whom you transferring money (6 numbers)");
                                passport = Console.ReadLine() !;
                                var client2 = tinkoff.GetClientByPassport(passport);
                                var accountTo = tinkoff.GetAccountByClient(client2);
                                Console.WriteLine("Enter amount of money you transferring");
                                money = Convert.ToDecimal(Console.ReadLine());
                                var transfer = new TransferCommand(account, accountTo, money);
                                tinkoff.SetCommand(transfer);
                                tinkoff.RunCommand();
                                Console.WriteLine("Now your balance: " + account.GetBalance());
                                Console.WriteLine("Enter '1' if you want to undo this transaction");
                                Console.WriteLine("Enter '2' if you want to continue");
                                var f = Console.ReadLine();
                                switch (f)
                                {
                                    case "1":
                                        tinkoff.CancelCommand(transfer);
                                        Console.WriteLine("Now your balance: " + account.GetBalance());
                                        break;
                                    case "2":
                                        break;
                                }

                                break;
                            case "4":
                                Console.WriteLine("Enter your passport id (6 numbers)");
                                passport = Console.ReadLine() !;
                                client = tinkoff.GetClientByPassport(passport);
                                tinkoff.AddSubscriber(client);
                                break;
                            case "5":
                                Console.WriteLine("Enter your passport id (6 numbers)");
                                passport = Console.ReadLine() !;
                                client = tinkoff.GetClientByPassport(passport);
                                account = tinkoff.GetAccountByClient(client);
                                Console.WriteLine("Enter amount of days you want to skip");
                                var timeToSkip = Convert.ToInt32(Console.ReadLine());
                                centralBank.TimeRemote(timeToSkip);
                                Console.WriteLine("Now your balance: " + account.GetBalance());
                                break;
                        }

                        break;
                    case "4":
                        Console.WriteLine("Enter new debit percent");
                        var newDebitPercent = Convert.ToInt32(Console.ReadLine());
                        tinkoff.ChangeDebitPercent(newDebitPercent);
                        break;
                }
            }
        }
    }
}