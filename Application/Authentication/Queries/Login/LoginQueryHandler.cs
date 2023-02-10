using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Database.Repositories;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IRepository<User> _userRepository;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHelper passwordHelper,
        IRepository<User> userRepository
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHelper = passwordHelper;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        if (await _userRepository.NoTrackingQuery()
                .SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken) is not { } user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (!_passwordHelper.VerifyHashedPassword(user.Password, request.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }


        if (!_passwordHelper.VerifyHashedPassword(user.Password, request.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(user);

        return new AuthenticationResult(user, token, refreshToken);
    }
}