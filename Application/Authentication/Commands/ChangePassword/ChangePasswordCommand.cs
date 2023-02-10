using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ChangePassword;

public record ChangePasswordCommand(
    Guid UserId,
    string CurrentPassword,
    string NewPassword
) : IRequest<ErrorOr<Success>>;