using Banks.Exceptions;

namespace Banks.Entities
{
    public class WithdrawCommand : ICommand
    {
        private readonly IBankAccount _recipient;
        private readonly decimal _money;
        private bool _undoAvailable = false;

        public WithdrawCommand(IBankAccount recipient, decimal money)
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

            _recipient.Withdraw(_money);
            _undoAvailable = true;
        }

        public void Undo()
        {
            if (_undoAvailable)
            {
                _recipient.TopUp(_money);
            }
        }
    }
}