namespace Api.Dtos.Authentication;

public record SignUpDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);