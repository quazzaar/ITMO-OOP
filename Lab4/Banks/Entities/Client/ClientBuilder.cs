using Banks.Exceptions;

namespace Banks.Entities;

public class ClientBuilder : IClientBuilder
{
    private string? _name;
    private string? _surname;
    private string? _address;
    private string? _passport;
    private bool isSuspicious;

    public IClientBuilder SetName(string? name)
    {
        _name = name;
        return this;
    }

    public IClientBuilder SetSurname(string? surname)
    {
        _surname = surname;
        return this;
    }

    public IClientBuilder SetAddress(string? address)
    {
        _address = address;
        return this;
    }

    public IClientBuilder SetPassport(string? passport)
    {
        _passport = passport;
        return this;
    }

    public Client Build()
    {
        if (string.IsNullOrEmpty(_name) || string.IsNullOrEmpty(_surname))
        {
            throw new InvalidClientException("Fields name and surname are required");
        }

        if (string.IsNullOrEmpty(_address) || string.IsNullOrEmpty(_passport))
        {
            isSuspicious = true;
        }

        return new Client(_name, _surname, _address, _passport, isSuspicious);
    }
}