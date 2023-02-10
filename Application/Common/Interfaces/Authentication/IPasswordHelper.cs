namespace Application.Common.Interfaces.Authentication;

public interface IPasswordHelper
{
    public string HashPassword(string password);

    public bool VerifyHashedPassword(string hashedPassword, string password);
}