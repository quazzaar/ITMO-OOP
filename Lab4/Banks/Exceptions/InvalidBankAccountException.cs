namespace Banks.Exceptions;

public class InvalidBankAccountException : Exception
{
    public InvalidBankAccountException(string message)
        : base(message) { }
}