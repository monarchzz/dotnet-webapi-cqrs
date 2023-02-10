namespace Api.Dtos.Authentication;

public record ChangePasswordDto(string CurrentPassword, string NewPassword);