namespace Banks.Exceptions;

public class InvalidClientException : Exception
{
    public InvalidClientException(string message)
        : base(message) { }
}