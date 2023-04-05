namespace Banks.Entities;

public interface IObservable
{
    void AddSubscriber(IObserver observer);
    void RemoveSubscriber(IObserver observer);
    void Notify();
}