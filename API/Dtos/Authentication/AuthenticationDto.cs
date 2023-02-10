namespace Api.Dtos.Authentication;

public record AuthenticationDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token,
    string RefreshToken
);