using System;
using Banks.Exceptions;

namespace Banks.Entities
{
    public class TopUpCommand : ICommand
    {
        private readonly IBankAccount _recipient;
        private readonly decimal _money;
        private bool _undoAvailable = false;

        public TopUpCommand(IBankAccount recipient, decimal money)
        {
            _recipient = recipient;
            _money = money;
        }

        public void Execute()
        {
            if (_recipient == null)
            {
                throw new InvalidTransactionException("This command can not be completed");
            }

            _recipient.TopUp(_money);
            _undoAvailable = true;
        }

        public void Undo()
        {
            if (_undoAvailable)
            {
                _recipient.Withdraw(_money);
            }
        }
    }
}