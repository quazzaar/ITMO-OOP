namespace Banks.Entities;

public interface IClientBuilder
{
    IClientBuilder SetName(string? name);
    IClientBuilder SetSurname(string? surname);
    IClientBuilder SetAddress(string? address);
    IClientBuilder SetPassport(string? passport);
    Client Build();
}