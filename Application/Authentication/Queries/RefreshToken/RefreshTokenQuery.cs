using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.RefreshToken;

public record RefreshTokenQuery(string RefreshToken) : IRequest<ErrorOr<AuthenticationResult>>;