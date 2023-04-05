using System;
using System.Collections.Generic;
using System.Linq;
using MessagingSystem;
using MessagingSystem.Application;
using MessagingSystem.DataAccess;
using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var employees = new List<Employee>();
            var messages = new List<Message>();
            var accounts = new List<Account>();
            var reports = new List<Report>();

            var messageSystem =
                new MessageSystemService(
                    "123",
                    employees,
                    messages,
                    accounts,
                    reports);
            var messageSystemDecorator = new MessageSystemDecorator(messageSystem);
            messageSystemDecorator.Start(
                "123",
                accounts,
                employees,
                messages,
                reports);
        }
    }
}