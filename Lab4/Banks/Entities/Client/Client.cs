namespace Banks.Entities;

public class Client : IObserver
{
    public Client(string? name, string? surname, string? address, string? passport, bool isSuspicious)
    {
        Name = name;
        Surname = surname;
        Address = address;
        Passport = passport;
        IsSuspicious = isSuspicious;
    }

    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Address { get; set; }
    public string? Passport { get; set; }
    public bool IsSuspicious { get; set; }

    public void Update(IObservable observable)
    {
        Console.WriteLine("Dear " + Name + " " + Surname + " we have some updates in bank account conditions");
    }
}