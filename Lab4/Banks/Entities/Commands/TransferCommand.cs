using Banks.Exceptions;

namespace Banks.Entities
{
    public class TransferCommand : ICommand
    {
        private readonly IBankAccount _accountFrom;
        private readonly IBankAccount _accountTo;
        private readonly decimal _money;
        private bool _undoAvailable = false;

        public TransferCommand(IBankAccount accountFrom, IBankAccount accountTo, decimal money)
        {
            _accountFrom = accountFrom;
            _accountTo = accountTo;
            _money = money;
        }

        public void Execute()
        {
            if (_accountFrom == null && _accountTo == null)
            {
                throw new InvalidTransactionException("This command can not be completed");
            }

            _accountFrom?.Transfer(_accountTo, _money);
            _undoAvailable = true;
        }

        public void Undo()
        {
            if (_undoAvailable)
            {
                _accountFrom.TopUp(_money);
                _accountTo.Withdraw(_money);
            }
        }
    }
}