using Application.Common.Interfaces.Authentication;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Authentication;

public class PasswordHelper : IPasswordHelper
{
    public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }

    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        return BC.Verify(password, hashedPassword);
    }
}