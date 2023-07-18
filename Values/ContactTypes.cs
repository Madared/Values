namespace Values;
using Results;

public class Email : Contact
{
    private string _name;
    private string _domain;
    private string _countryIdentifier;

    private Email(string name, string domain, string countryIdentifier)
    {
        _name = name;
        _domain = domain;
        _countryIdentifier = countryIdentifier;
    }
    public static implicit operator string(Email email) => $"{email._name}@{email._domain}.{email._countryIdentifier}";

    private static string? AfterAtSign(string email) => email.Split('@')[1] ?? null;
    private static string? GetName(string email) => email.Split('@')[0] ?? null;
    private static string? GetDomain(string email) =>
        AfterAtSign(email)
            .Pipe(after => after.Split('.')[0]);

    private static string? GetCountryIdentifier(string email) =>
        AfterAtSign(email)
            .Pipe(after => after.Split('.')[1]);

    public static Result<Email> Create(string email)
    {
        string? name = GetName(email);
        string? domain = GetDomain(email);
        string? countryIdentifier = GetCountryIdentifier(email);
        if (name is null || domain is null || countryIdentifier is null)
            return Result<Email>.Fail(new InvalidEmailFormat(email));

        return Result<Email>.Ok(new Email(name, domain, countryIdentifier));
    }

    public string Get() => $"{_name}@{_domain}.{_countryIdentifier}";
}

public class PhoneNumber : Contact
{
    private int _countryCode;
    private int _number;

    public PhoneNumber(int countryCode, int number)
    {
        _countryCode = countryCode;
        _number = number;
    }

    public string Get() => $"+{_countryCode} {_number}";
}

public interface Contact
{
    string Get();
}

public class InvalidEmailFormat : IError
{
    private string _message; 
    public string Message => _message;
    public InvalidEmailFormat(string email)
    {
        _message = String.Format("{0} has an invalid email format");
    }

    public void Log() => throw new NotImplementedException();
}
