namespace MessagingSystem.DataAccess.Entities;

public class Message
{
    public Message(string messageText, MessageSource.SourceOfMessage source, Employee recipient, MessageState state)
    {
        MessageText = messageText;
        Source = source;
        Recipient = recipient;
        State = state;
        Id = Guid.NewGuid();
    }

    public enum MessageState
    {
        New = 0,
        Received = 1,
        Processed = 2,
    }

    public string MessageText { get; set; }
    public MessageSource.SourceOfMessage Source { get; set; }
    public Employee Recipient { get; set; }
    public MessageState State { get; set; }
    public Guid Id { get; }
}