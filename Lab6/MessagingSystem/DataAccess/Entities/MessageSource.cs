namespace MessagingSystem.DataAccess.Entities;

public class MessageSource
{
    public MessageSource(SourceOfMessage source)
    {
        Source = source;
    }

    public enum SourceOfMessage
    {
        Email = 0,
        Sms = 1,
        Messenger = 2,
    }

    public SourceOfMessage Source { get; set; }
}