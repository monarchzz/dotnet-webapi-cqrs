using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Database.Repositories;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.SignUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IRepository<User> _userRepository;

    public SignUpCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHelper passwordHelper,
        IRepository<User> userRepository
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHelper = passwordHelper;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        SignUpCommand command,
        CancellationToken cancellationToken
    )
    {
        if (await _userRepository.NoTrackingQuery(u => u.Email == command.Email)
                .SingleOrDefaultAsync(cancellationToken) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User(
            firstName: command.FirstName,
            lastName: command.LastName,
            email: command.Email,
            password: _passwordHelper.HashPassword(command.Password)
        );

        await _userRepository.Add(user, cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(user);

        return new AuthenticationResult(user, token, refreshToken);
    }
}