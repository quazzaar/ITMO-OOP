namespace MessagingSystem.Application.Exceptions;

public class MessageException : Exception
{
    public MessageException(string message)
        : base(message) { }
}