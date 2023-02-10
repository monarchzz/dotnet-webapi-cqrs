using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Database.Repositories;
using Domain.Common.Errors;
using Domain.Entities;
using MediatR;
using ErrorOr;

namespace Application.Authentication.Queries.RefreshToken;

public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRepository<User> _userRepository;

    public RefreshTokenQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IRepository<User> userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RefreshTokenQuery query,
        CancellationToken cancellationToken
    )
    {
        var userId = _jwtTokenGenerator.VerifyRefreshToken(query.RefreshToken);
        if (userId == null) return Errors.Authentication.TokenExpiredOrInvalid;

        var user = await _userRepository.Find(userId.Value, cancellationToken);
        if (user == null) return Errors.Authentication.InvalidCredentials;

        var token = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(user);

        return new AuthenticationResult(user, token, refreshToken);
    }
}