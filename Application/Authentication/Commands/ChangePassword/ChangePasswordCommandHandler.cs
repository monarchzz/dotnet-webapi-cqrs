using Application.Common.Interfaces.Authentication;
using Database.Repositories;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<Success>>
{
    private readonly IPasswordHelper _passwordHelper;
    private readonly IRepository<User> _userRepository;

    public ChangePasswordCommandHandler(IPasswordHelper passwordHelper, IRepository<User> userRepository)
    {
        _passwordHelper = passwordHelper;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Success>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Find(request.UserId, cancellationToken);
        if (user == null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (!_passwordHelper.VerifyHashedPassword(user.Password, request.CurrentPassword))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        user.Password = _passwordHelper.HashPassword(request.NewPassword);
        await _userRepository.Update(user, cancellationToken);

        return Result.Success;
    }
}