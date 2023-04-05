namespace Banks.Entities;

public interface IObserver
{
    void Update(IObservable observable);
}